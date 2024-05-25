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
        /// 로그 추가
        /// </summary>
        protected override void AddLogMessage(ILogMessage message)
        {
            SetLogItem(message);
        }

        [SerializeField] private LogItem[] logItems; // 로그 아이템 배열
        private int currentIndex = 0; // 현재 설정 로그 아이템 인덱스

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
                LogItem source = logItems[0];

                List<LogItem> list = new List<LogItem>(logItems);

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
        private LogItem CreateLogItem(int index, LogItem source)
        {
            LogItem clone = CreateLogItem(source);

            clone.SetBackgroundColor(GetBackgroundColor(index));

            return clone;
        }

        /// <summary>
        /// 로그 아이템 생성
        /// </summary>
        private LogItem CreateLogItem(LogItem source)
        {
            return Instantiate(source, source.Parent);
        }
        #endregion
    }
}