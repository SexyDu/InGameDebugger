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

        public void Subscribe(IConsoleLogObserver observer)
        {
            if (logObservers.Contains(observer))
            {
                Debug.LogWarningFormat("이미 존재하는 옵저버가 등록을 시도했습니다. Type : {0}", observer.GetType());
            }
            else
                logObservers.Add(observer);
        }

        public void Unsubscribe(IConsoleLogObserver observer)
        {
            logObservers.Remove(observer);
        }

        private void CollectLogCount(LogType type)
        {
            switch (InGameDebuggerConfig.Ins.ToGeneralType(type))
            {
                case LogType.Error:
                    logCount++;
                    break;
                case LogType.Warning:
                    warningCount++;
                    break;
                case LogType.Log:
                    errorCount++;
                    break;
            }

            NotifyDetectLog(type);
        }

        private void NotifyDetectLog(LogType type)
        {
            for (int i = 0; i < logObservers.Count; i++)
            {
                logObservers[i].OnDetectLog(this, type);
            }
        }
    }

    public interface IConsoleLogObserver
    {
        public void OnDetectLog(IConsoleLogSubject subject, LogType type);
    }

    public interface IConsoleLogSubject
    {
        public void Subscribe(IConsoleLogObserver observer);
        public void Unsubscribe(IConsoleLogObserver observer);
    }
}