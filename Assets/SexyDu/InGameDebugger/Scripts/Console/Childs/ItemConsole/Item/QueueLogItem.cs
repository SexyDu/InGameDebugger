#define USE_SLIDER

using System;
using UnityEngine;
#if USE_SLIDER
using SexyDu.UI.UGUI;
#endif

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 로그 아이템  Queue 관리 (변형 Queue)
    /// </summary>
    [Serializable]
    public class QueueLogItem : IClearable, IReleasable
    {
        [SerializeField] private float itemHeight; // 아이템별 높이값
        private ILogItem[] items = null; // 로그 아이템 배열

        // 로그 아이템 수
        public int Count => items.Length;

        public void Initialize(int count)
        {
            if (items == null)
                Initialize(CreateLogItems(count));
            else
                Debug.LogWarning("이미 LogItem이 구성되었습니다.");
        }

        /// <summary>
        /// 로그 아이템 설정
        /// </summary>
        private void Initialize(ILogItem[] items)
        {
            this.items = items;
        }

        /// <summary>
        /// 메세지 인입
        /// </summary>
        public void Enqueue(ILogMessage message)
        {
            current = GetNextIndex(current);

            items[current].Set(message);
        }

        /// <summary>
        /// 전체 메세지 클리어
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Clear();
            }

            // 활성화 수 0으로 설정
            SetActivatedCount(0);
        }

        /// <summary>
        /// 해제
        /// </summary>
        public void Release()
        {
            Clear();

            items = null;
        }

        /// <summary>
        /// Queue 아이템 정렬
        /// </summary>
        public void Range()
        {
            if (current < 0)
                return;

            // 현재 인덱스
            int index = current;

            // 아이템 위치값 저장소
            Vector2 anchoredPosition = Vector2.zero;
            // 활성화 아이템 수
            int activatedCount = 0;

            do
            {
                // 현재 아이템 위치값 설정
                items[index].anchoredPosition = anchoredPosition;

                // 위치값 증가
                anchoredPosition.y += itemHeight;

                // 활성화 아이템 수 증가
                activatedCount++;

                // 이전 인덱스 설정
                index = GetPreviousIndex(index);

            } while (!index.Equals(current) && items[index].Activated); // 인덱스와 현재와 같아지지 않고 현재 인덱스 아이템이 활성화 되어있는 경우 반복

#if USE_SLIDER

            // 활성화 아이템 수 설정
            SetActivatedCount(activatedCount);

#else
            Vector2 sizeDelta = parent.sizeDelta;
            sizeDelta.y = totalHeight;
            parent.sizeDelta = sizeDelta;
#endif
        }

        #region Index
        // Queue의 현재 인덱스
        private int current = -1;
        // 아이템 총 활성화 수
        private int activatedCount = 0;

        /// <summary>
        /// 이전 인덱스 반환
        /// </summary>
        private int GetPreviousIndex(int current)
        {
            current--;

            if (current < 0)
                current = items.Length - 1;

            return current;
        }

        /// <summary>
        /// 다음 인덱스 반환
        /// </summary>
        private int GetNextIndex(int current)
        {
            current++;

            if (current >= items.Length)
                current = 0;

            return current;
        }

        /// <summary>
        /// 활성화 아이템 수 설정
        /// </summary>
        private void SetActivatedCount(int activatedCount)
        {
            // 현재 수와 설정하려는 수가 다른 경우
            if (!this.activatedCount.Equals(activatedCount))
            {
                // 설정 및 slider 제한 설정
                this.activatedCount = activatedCount;

                // 활성화 아이템 수 변경에 따른 슬라이더 제한영역 설정
                SetSliderLimit(this.activatedCount);
            }
        }
        #endregion

        #region ContentsParent
        [Header("ContentsParent")]
        /// UGUI의 ScrollView 동작을 위해 크기가 설정될 오브젝트
        [SerializeField] private RectTransform parent;
        #endregion

#if USE_SLIDER
        #region Slider
        [Header("Slider")]
        [SerializeField] private VerticalSliderLight slider; // 스크롤뷰

        /// <summary>
        /// 슬라이더의 제한값 설정
        /// </summary>
        public void RefreshSliderLimit()
        {
            SetSliderLimit(activatedCount);
        }

        /// <summary>
        /// 슬라이더의 제한값 설정
        /// </summary>
        private void SetSliderLimit(int activatedCount)
        {
            // 활성화 아이템 영역 높이
            float height = itemHeight * activatedCount;
            // 활성화 아이템 영역 및 스크롤뷰 영역을 고려하여 min 계산
            float min = slider.Area.y - height;

            //Debug.LogFormat("rect.height ({0}) - height ({1}) = min ({2})", slideAreaRect.rect.height, height, min);
            if (min > 0)
                min = 0;
            slider.SetMinimum(min);
        }

        /// <summary>
        /// 제한수치에 따른 슬라이더 위치값 보정
        /// </summary>
        public void AmendSliderPosition()
        {
            slider.AmendTargetPosition();
        }
        #endregion
#endif

        #region Create
        [Header("Create Log Item")]
        [SerializeField] private Color[] backgroundColors; // 로그 아이템 배경 색상

        /// <summary>
        /// 인덱스에 따른 배경 색상 반환
        /// </summary>
        private Color GetBackgroundColor(int index)
        {
            return backgroundColors[index % backgroundColors.Length];
        }

        /// <summary>
        /// 지정된 수에 따른 로그 아이템 생성
        /// </summary>
        private ILogItem[] CreateLogItems(int targetCount)
        {
            LogItemSource source = Resources.Load<LogItemSource>(LogItemSource.ResourcePath);
            if (source == null)
                throw new NullReferenceException($"지정된 경로({LogItemSource.ResourcePath})에 로그아이템이 존재하지 않습니다.");

            ILogItem[] items = new ILogItem[targetCount];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = CreateLogItem(source).SetBackgroundColor(GetBackgroundColor(i));
            }

            return items;
        }

        /// <summary>
        /// 로그 아이템 생성
        /// - 기본 클래스 형식 아이템
        /// </summary>
        private LogItem CreateLogItem(LogItemSource source)
        {
            return MonoBehaviour.Instantiate(source, parent).ConvertLogItem(slider);
        }
        #endregion
    }
}