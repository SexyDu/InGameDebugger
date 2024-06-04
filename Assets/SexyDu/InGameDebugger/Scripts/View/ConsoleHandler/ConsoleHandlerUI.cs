using UnityEngine;
using UnityEngine.UI;

namespace SexyDu.InGameDebugger.View
{
    public abstract class ConsoleHandlerUI : MonoBehaviour, IConsoleHandlerUI
    {
        protected abstract ConsoleHandler BaseHandler { get; }

        #region OnAwakeInit
        [SerializeField] private bool onAwakeInit = true;
        private void Awake()
        {
            if (onAwakeInit)
                Initialize();
        }

        /// <summary>
        /// 초기 설정
        /// </summary>
        protected virtual void Initialize()
        {
            // 필터에 팔로워를 전달하며 초기 설정
            filter.Initialize(BaseHandler.FilterFollower, BaseHandler.ConsoleLogSubject);

            // 앵커 초기 설정
            anchor.Initialize();
        }
        #endregion

        /// <summary>
        /// UI 갱신
        /// </summary>
        public virtual void Refresh()
        {
            RefreshPlayState();
        }

        #region PlayState
        [Header("PlayState")]
        [SerializeField] private Image imagePlayState;
        [SerializeField] private Sprite spritePlay;
        [SerializeField] private Sprite spritePause;

        private void Play()
        {
            BaseHandler.PauseConsole(false);

            RefreshPlayState();
        }

        private void Pause()
        {
            BaseHandler.PauseConsole(true);

            RefreshPlayState();
        }

        public void OnClickTogglePlay()
        {
            if (BaseHandler.Playing)
                Pause();
            else
                Play();
        }

        private void RefreshPlayState()
        {
            SetPlayState(BaseHandler.Playing);
        }

        private void SetPlayState(bool playing)
        {
            imagePlayState.sprite = playing ? spritePause : spritePlay;
        }
        #endregion

        #region Clear
        public void OnClickClearLog()
        {
            BaseHandler.ClearConsole();
        }
        #endregion

        #region Minimize
        public void OnClickMinimize()
        {
            BaseHandler.Inactivate();
        }
        #endregion

        #region Filter
        [Header("Filter")]
        [SerializeField] private ConsoleFilterUI filter;
        #endregion

        #region Anchor
        [Header("Anchor")]
        [SerializeField] private ConsoleAnchorUI anchor;
        #endregion
    }
}