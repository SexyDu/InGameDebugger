using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 로그 수집에 따른 옵저버패턴
    /// </summary>
    public abstract partial class Console : IConsoleLogSubject
    {
        private List<IConsoleLogObserver> logObservers = new List<IConsoleLogObserver>();

        private int logCount = 0;
        private int warningCount = 0;
        private int errorCount = 0;
        public int LogCount { get => logCount; }
        public int WarningCount { get => warningCount; }
        public int ErrorCount { get => errorCount; }

        /// <summary>
        /// 로그 카운트 클리어
        /// </summary>
        public void ClearLogCount()
        {
            logCount = 0;
            warningCount = 0;
            errorCount = 0;

            NotifyDetectLog();
        }

        /// <summary>
        /// 로그 옵저버 등록
        /// </summary>
        public void Subscribe(IConsoleLogObserver observer)
        {
            if (logObservers.Contains(observer))
            {
                Debug.LogWarningFormat("이미 존재하는 옵저버가 등록을 시도했습니다. Type : {0}", observer.GetType());
            }
            else
                logObservers.Add(observer);
        }

        /// <summary>
        /// 로그 옵저버 해제
        /// </summary>
        public void Unsubscribe(IConsoleLogObserver observer)
        {
            logObservers.Remove(observer);
        }

        /// <summary>
        /// 로그 카운트 추가
        /// </summary>
        private void CollectLogCount(LogType type)
        {
            switch (InGameDebuggerConfig.Ins.ToGeneralType(type))
            {
                case LogType.Log:
                    logCount++;
                    break;
                case LogType.Warning:
                    warningCount++;
                    break;
                case LogType.Error:
                    errorCount++;
                    break;
            }

            NotifyDetectLog(type);
        }

        /// <summary>
        /// 로그 옵저버 노티
        /// </summary>
        private void NotifyDetectLog(LogType type)
        {
            for (int i = 0; i < logObservers.Count; i++)
            {
                logObservers[i].OnDetectLog(this, type);
            }
        }

        /// <summary>
        /// 로그 옵저버 노티
        /// </summary>
        private void NotifyDetectLog()
        {
            for (int i = 0; i < logObservers.Count; i++)
            {
                logObservers[i].OnDetectLog(this);
            }
        }
    }
}