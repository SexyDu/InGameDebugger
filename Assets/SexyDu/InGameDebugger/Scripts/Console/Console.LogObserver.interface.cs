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
    }

    /// <summary>
    /// 콘솔 로그 옵저버 인터페이스
    /// </summary>
    public interface IConsoleLogObserver
    {
        public void OnDetectLog(IConsoleLogSubject subject, LogType type);
    }
}