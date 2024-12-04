using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 콘솔 로그 서브젝트 인터페이스
    /// </summary>
    public interface IConsoleLogSubject
    {
        public void Subscribe(IConsoleLogObserver observer);
        public void Unsubscribe(IConsoleLogObserver observer);

        public int LogCount { get; }
        public int WarningCount { get; }
        public int ErrorCount { get; }
        public int GeneralErrorCount { get; }
    }

    /// <summary>
    /// 콘솔 로그 옵저버 인터페이스
    /// </summary>
    public interface IConsoleLogObserver
    {
        /// <summary>
        /// 로그 카운트 변경 감지
        /// * Clear 등
        /// </summary>
        public void OnDetectLog(IConsoleLogSubject subject);

        /// <summary>
        /// 로그 카운트 추가 감지
        /// </summary>
        public void OnDetectLog(IConsoleLogSubject subject, LogType type);
    }
}