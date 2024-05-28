using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public abstract partial class Console : MonoBehaviour, IClearable
    {
        // 수집 상태
        private bool playing = false;
        public bool Playing { get => playing; }

        /// <summary>
        /// 로깅 시작
        /// </summary>
        public virtual void Play()
        {
            playing = true;

            Application.logMessageReceived += OnLogMessageReceived;
        }

        /// <summary>
        /// 로깅 정지
        /// </summary>
        public virtual void Pause()
        {
            playing = false;

            Application.logMessageReceived -= OnLogMessageReceived;
        }

        /// <summary>
        /// 콘솔 클리어
        /// : IClearable
        /// </summary>
        public virtual void Clear()
        {
            messages.Clear();
        }

        // 로그 메세지 리스트
        protected List<ILogMessage> messages = new List<ILogMessage>();

        /// <summary>
        /// 로깅 메세지 이벤트
        /// </summary>
        protected virtual void OnLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            ILogMessage message = new LogMessage(condition, stackTrace, type);

            AddLogMessage(message);

            // 로그 수 수집 (for 로그 옵저버)
            CollectLogCount(type);
        }

        /// <summary>
        /// 초기 설정
        /// </summary>
        public abstract Console Initialize();

        /// <summary>
        /// 로그 추가
        /// </summary>
        protected virtual void AddLogMessage(ILogMessage message)
        {
            messages.Add(message);
        }

        /// <summary>
        /// 로그 화면 갱신
        /// </summary>
        public abstract void RefreshLogDisplay();

        #region LogType Activation
        // 활성화된 로그 타입
        private List<LogType> activatedLogTypes = new List<LogType>(
            new LogType[] { LogType.Log, LogType.Warning, LogType.Error, LogType.Assert, LogType.Exception }
            );

        /// <summary>
        /// 해당 로그 타입이 포함되어 있는지 여부 반환
        /// </summary>
        protected bool ContainLogType(LogType type)
        {
            return activatedLogTypes.Contains(type);
        }

        /// <summary>
        /// 활성화 로그 타입 추가
        /// </summary>
        public void ActivateLogType(params LogType[] logTypes)
        {
            for (int i = 0; i < logTypes.Length; i++)
            {
                if (!activatedLogTypes.Contains(logTypes[i]))
                    activatedLogTypes.Add(logTypes[i]);
            }

            RefreshLogDisplay();
        }

        /// <summary>
        /// 활성화 로그 타입 제외
        /// </summary>
        public void InactivateLogType(params LogType[] logTypes)
        {
            for (int i = 0; i < logTypes.Length; i++)
            {
                activatedLogTypes.Remove(logTypes[i]);
            }

            RefreshLogDisplay();
        }
        #endregion
    }
}