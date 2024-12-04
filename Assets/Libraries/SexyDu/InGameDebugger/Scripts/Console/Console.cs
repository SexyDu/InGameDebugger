using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public abstract partial class Console : MonoBehaviour, IClearable, IReleasable 
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
        /// 클리어
        /// : IClearable
        /// </summary>
        public virtual void Clear()
        {
            messages.Clear();

            ClearLogCount();
        }

        /// <summary>
        /// 해제
        /// </summary>
        public virtual void Release()
        {
            Pause();

            Clear();
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

        #region Display Status
        // 콘솔 표시 상태
        private bool isDisplaying = false;
        protected bool IsDisplaying => isDisplaying;

        /// <summary>
        /// 콘솔 표시 상태 설정
        /// </summary>
        public virtual void SetDisplayStatus(bool isDisplaying)
        {
            this.isDisplaying = isDisplaying;
        }
        #endregion
    }
}