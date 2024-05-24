using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public abstract class Console : MonoBehaviour
    {
        /// <summary>
        /// 로깅 시작
        /// </summary>
        public virtual void Play()
        {
            Application.logMessageReceived += OnLogMessageReceived;
        }

        /// <summary>
        /// 로깅 정지
        /// </summary>
        public virtual void Pause()
        {
            Application.logMessageReceived -= OnLogMessageReceived;
        }

        /// <summary>
        /// 로깅 메세지 이벤트
        /// </summary>
        protected virtual void OnLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            ILogMessage message = new LogMessage(condition, stackTrace, type);

            AddLogMessage(message);
        }

        /// <summary>
        /// 로그 추가
        /// </summary>
        protected abstract void AddLogMessage(ILogMessage message);
    }
}