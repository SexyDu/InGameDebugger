using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameConsole
{
    public class TestItemConsole : Console
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

        protected override void AddLogMessage(ILogMessage message)
        {
            SetLogItem(message);
        }

        public void Initialize()
        {
            CreateLogItems(InGameConsoleConfig.Ins.Settings.ItemCount);
        }

        [SerializeField] private LogItem[] logItems;
        private int currentIndex = 0;

        private void SetLogItem(ILogMessage message)
        {
            SetLogItem(currentIndex, message);

            Next();
        }

        private void SetLogItem(int index, ILogMessage message)
        {
            logItems[index].Set(message);
        }

        private void Next()
        {
            currentIndex++;

            if (currentIndex >= logItems.Length)
                currentIndex = 0;
        }

        #region CreateLogItem
        [Header("Create Log Item")]
        [SerializeField] private Color[] backgroundColors;

        private Color GetBackgroundColor(int index)
        {
            return backgroundColors[index % backgroundColors.Length];
        }

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

        private LogItem CreateLogItem(int index, LogItem source)
        {
            LogItem clone = CreateLogItem(source);

            clone.SetBackgroundColor(GetBackgroundColor(index));

            return clone;
        }

        private LogItem CreateLogItem(LogItem source)
        {
            return Instantiate(source, source.Parent);
        }
        #endregion
    }
}