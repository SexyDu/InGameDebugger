using System;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 로그 아이템  Queue 관리 (변형 Queue)
    /// </summary>
    [Serializable]
    public class QueueLogItem : IClearable, IReleasable
    {
        [SerializeField] private float height; // 아이템별 높이값
        private int current = -1; // 현재 인덱스
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
            int target = current + 1;
            if (target >= items.Length)
                target = 0;

            items[target].Set(message);
            current = target;
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
            int index = current;

            Vector2 anchoredPosition = Vector2.zero;
            float totalHeight = 0f;
            
            do
            {
                items[index].anchoredPosition = anchoredPosition;
                anchoredPosition.y += height;
                totalHeight = anchoredPosition.y;

                index--;
                if (index < 0)
                    index = items.Length - 1;
            } while (!index.Equals(current) && items[index].Activated);

            Vector2 sizeDelta = parent.sizeDelta;
            sizeDelta.y = totalHeight;
            parent.sizeDelta = sizeDelta;
        }

        #region ContentsParent
        [Header("ContentsParent")]
        /// UGUI의 ScrollView 동작을 위해 크기가 설정될 오브젝트
        [SerializeField] private RectTransform parent;
        #endregion

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
            return MonoBehaviour.Instantiate(source, parent).ConvertLogItem();
        }
        #endregion
    }
}