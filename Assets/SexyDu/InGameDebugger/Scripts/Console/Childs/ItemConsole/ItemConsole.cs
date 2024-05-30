//#define USE_MONOITEM
#if UNITY_EDITOR
#define ONLY_EDITOR
#endif

using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ItemConsole : Console
    {
        #region OnAwake
        [SerializeField] private bool onAwakeInit;
        [SerializeField] private bool onAwakePlay;

        private void Awake()
        {
            if (onAwakeInit)
            {
                Initialize();

                if (onAwakePlay)
                    Play();
            }
        }
        #endregion
        
        /// <summary>
        /// 초기 설정
        /// </summary>
        public override Console Initialize()
        {
            CreateLogItems(InGameDebuggerConfig.Ins.Settings.ItemCount);

            return this;
        }

        /// <summary>
        /// 로그 클리어
        /// </summary>
        public override void Clear()
        {
            ClearLogItems();

            base.Clear();
        }

        /// <summary>
        /// 로그 추가
        /// </summary>
        protected override void AddLogMessage(ILogMessage message)
        {
            base.AddLogMessage(message);

            DisplayLogItem(message);
        }

        /// <summary>
        /// 로그 화면 갱신
        /// </summary>
        public override void RefreshLogDisplay()
        {
            ClearLogItems();

            // 갱신 메세지 스택
            Stack<ILogMessage> refreshMessages = new Stack<ILogMessage>();

            for (int i = messages.Count - 1; i >= 0; i--)
            {
                if (PassFilters(messages[i]))
                {
                    refreshMessages.Push(messages[i]);

                    // 갱신 메세지 수가 로그 아이템과 같거나 큰 경우 더 이상 필요 없기에 반복문 탈출
                    if (refreshMessages.Count >= logItems.Length)
                        break;
                }
            }

            while (refreshMessages.Count > 0)
            {
                SetLogItem(refreshMessages.Pop());
            }
        }

        /// <summary>
        /// 로그 아이템 배열
        /// </summary>
        private ILogItem[] logItems;
        private int currentIndex = 0; // 현재 설정 로그 아이템 인덱스

        /// <summary>
        /// [필터링 포함]
        /// 로그 메세지를 로그 아이템에 설정
        /// </summary>
        private void DisplayLogItem(ILogMessage message)
        {
            // 필터에 통과된 경우만
            if (PassFilters(message))
                SetLogItem(message);
        }

        /// <summary>
        /// 로그 메세지를 로그 아이템에 설정
        /// </summary>
        private void SetLogItem(ILogMessage message)
        {
            SetLogItem(currentIndex, message);

            // 다음 인덱스로 변경
            Next();
        }

        /// <summary>
        /// 로그 메세지를 로그 아이템에 설정
        /// </summary>
        private void SetLogItem(int index, ILogMessage message)
        {
            logItems[index].Set(message);
        }

        /// <summary>
        /// 현재 설정 로그 아이템 인덱스를 다음 인덱스로 변경
        /// </summary>
        private void Next()
        {
            currentIndex++;

            if (currentIndex >= logItems.Length)
                currentIndex = 0;
        }

        /// <summary>
        /// 전체 로그아이템 클리어 함수
        /// </summary>
        private void ClearLogItems()
        {
            for (int i = 0; i < logItems.Length; i++)
            {
                logItems[i].Clear();
            }
        }

        #region CreateLogItem
        [Header("Create Log Item")]
        [SerializeField] private Transform itemParent; // 로그 아이템의 부모 Transform
        [SerializeField] private Color[] backgroundColors; // 로그 아이템 배경 색상

        /// <summary>
        /// LogItem의 source 로드
        /// </summary>
        private T LoadItemSource<T>(string resourcePath) where T : MonoBehaviour
        {
            return Resources.Load<T>(resourcePath);
        }

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
        public void CreateLogItems(int targetCount)
        {
            if (logItems == null)
                logItems = new ILogItem[0];

            if (logItems.Length >= targetCount)
            {
                Debug.LogWarningFormat($"이미 목표 아이템 수({targetCount}) 보다 많은 콘솔 아이템({logItems.Length})을 가지고 있습니다.");
            }
            else
            {
#if USE_MONOITEM
                MonoLogItem source = LoadItemSource<MonoLogItem>(MonoLogItem.ResourcePath);
#else
                LogItemSource source = LoadItemSource<LogItemSource>(LogItemSource.ResourcePath);
#endif

                List<ILogItem> list = new List<ILogItem>(logItems);

                while (list.Count < targetCount)
                {
                    // 로그 아이템을 생성하고 백그라운드를 한 뒤 리스트에 추가
                    list.Add(CreateLogItem(source).SetBackgroundColor(GetBackgroundColor(list.Count)));
                }

                logItems = list.ToArray();
            }
        }

#if USE_MONOITEM
        /// <summary>
        /// 로그 아이템 생성
        /// - MonoBehaviour 형식 아이템
        /// </summary>
        private MonoLogItem CreateLogItem(MonoLogItem source)
        {
            return Instantiate(source, itemParent);
        }
#else
        /// <summary>
        /// 로그 아이템 생성
        /// - 기본 클래스 형식 아이템
        /// </summary>
        private LogItem CreateLogItem(LogItemSource source)
        {
            return Instantiate(source, itemParent).ConvertLogItem();
        }
#endif
        #endregion

#if ONLY_EDITOR
        [Header("Only Editor")]
        /// 아이템 소스 (개발자 경로 확인용)
        /// * 실제 소스 로드는 Resource.Load(LogItemSource.ResourcePath) 를 통해 이루어집니다.
        [SerializeField] private LogItemSource logItemSource;
        /// 모노타입의 아이템 소스 (개발자 경로 확인용)
        /// * 실제 소스 로드는 Resource.Load(MonoLogItem.ResourcePath) 를 통해 이루어집니다.
        [SerializeField] private MonoLogItem logItemSourceMono;
#endif
    }
}