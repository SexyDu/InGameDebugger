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

        public override void RefreshLogDisplay()
        {
            ClearLogItems();

#if true
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
#else
            int lastItemIndex = currentIndex;

            for (int i = messages.Count - 1; i >= 0; i--)
            {
                if (ContainLogType(messages[i].Type))
                {
                    // 이전 인덱스로 만들기
                    /// * 여기서 이전인덱스로 먼저 만드는 이유
                    ///  : 최초 여기 들어왔을 시점의 currentIndex는 아직 메세지가 최신으로 설정되지 않은 인덱스이기 때문에
                    ///   현재의 최신 index인 Previous에 설정되도록 하기 위함
                    Previous();

                    SetLogItem(currentIndex, messages[i]);

                    // 현재 설정된 인덱스가 
                    if (currentIndex.Equals(lastItemIndex))
                        break;
                }
            }
#endif
        }

        [SerializeField] private MonoLogItem[] logItems; // 로그 아이템 배열
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
        public void CreateLogItems(int targetCount)
        {
            if (logItems.Length >= targetCount)
            {
                Debug.LogWarningFormat($"이미 목표 아이템 수({targetCount}) 보다 많은 콘솔 아이템({logItems.Length})을 가지고 있습니다.");
            }
            else
            {
                MonoLogItem source = logItems[0];

                List<MonoLogItem> list = new List<MonoLogItem>(logItems);

                while (list.Count < targetCount)
                {
                    list.Add(CreateLogItem(list.Count, source));
                }

                logItems = list.ToArray();
            }
        }

        /// <summary>
        /// 로그 아이템 생성
        /// </summary>
        private MonoLogItem CreateLogItem(int index, MonoLogItem source)
        {
            MonoLogItem clone = CreateLogItem(source);

            clone.SetBackgroundColor(GetBackgroundColor(index));

            return clone;
        }

        /// <summary>
        /// 로그 아이템 생성
        /// </summary>
        private MonoLogItem CreateLogItem(MonoLogItem source)
        {
            return Instantiate(source, source.Parent);
        }
        #endregion
    }
}