using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 로그 수집에 따른 옵저버패턴
    /// </summary>
    public abstract partial class Console : IConsoleLogSubject
    {
        /// <summary>
        /// 로크 카운트 옵저버 리스트
        /// </summary>
        private List<IConsoleLogObserver> logObservers = new List<IConsoleLogObserver>();

        /// <summary>
        /// 일반로그 수
        /// </summary>
        private int logCount = 0;
        public int LogCount { get => logCount; }

        /// <summary>
        /// 경고로그 수
        /// </summary>
        private int warningCount = 0;
        public int WarningCount { get => warningCount; }

        /// <summary>
        /// 에러로그 수
        /// </summary>
        private int errorCount = 0;
        public int ErrorCount { get => errorCount; }

        /// <summary>
        /// 강조로그 수
        /// </summary>
        private int assertionCount = 0;
        public int AssertionCount { get => assertionCount; }

        /// <summary>
        /// 예외로그 수
        /// </summary>
        private int exceptionCount = 0;
        public int ExceptionCount { get => exceptionCount; }

        /// <summary>
        /// 일반적인 에러로그 수
        /// </summary>
        public int GeneralErrorCount { get => errorCount + assertionCount + exceptionCount; }

        /// <summary>
        /// 로그 카운트 클리어
        /// </summary>
        public void ClearLogCount()
        {
            logCount = 0;
            warningCount = 0;
            errorCount = 0;
            assertionCount = 0;
            exceptionCount = 0;

            NotifyDetectLog();
        }

        /// <summary>
        /// 로그 옵저버 등록
        /// : IConsoleLogSubject
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
        /// : IConsoleLogSubject
        /// </summary>
        public void Unsubscribe(IConsoleLogObserver observer)
        {
            logObservers.Remove(observer);
        }

        /// <summary>
        /// 로그 카운트 추가
        /// </summary>
        protected void CollectLogCount(LogType type)
        {
            switch (type)
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
                case LogType.Assert:
                    assertionCount++;
                    break;
                case LogType.Exception:
                    exceptionCount++;
                    break;
            }

            NotifyDetectLog(type);
        }

        /// <summary>
        /// 신규 로그 추가에 따른 옵저버 노티
        /// </summary>
        private void NotifyDetectLog(LogType type)
        {
            for (int i = 0; i < logObservers.Count; i++)
            {
                logObservers[i].OnDetectLog(this, type);
            }
        }

        /// <summary>
        /// 로그 수 변경에 따른 옵저버 노티
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