using UnityEngine;
using UnityEngine.UI;
using SexyDu.UI.UGUI;

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
            anchor.Initialize(BaseHandler);
        }
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private Transform transformCache;
        protected Transform TransformCache => transformCache;
        #endregion

        /// <summary>
        /// UI 갱신
        /// : IConsoleHandlerUI
        /// </summary>
        public virtual void Refresh()
        {
            RefreshPlayState();
        }

        #region PlayState
        [Header("PlayState")]
        [SerializeField] private Image imagePlayState; // 플레이 상태 이미지 Component
        [SerializeField] private Sprite spritePlay; // 플레이 Sprite
        [SerializeField] private Sprite spritePause; // 정지 Sprite

        /// <summary>
        /// 콘솔 플레이
        /// </summary>
        private void Play()
        {
            BaseHandler.PauseConsole(false);
        }

        /// <summary>
        /// 콘솔 정지
        /// </summary>
        private void Pause()
        {
            BaseHandler.PauseConsole(true);
        }

        /// <summary>
        /// 콘솔 플레이 상태 토글 클릭
        /// </summary>
        public void OnClickTogglePlay()
        {
            if (BaseHandler.Playing)
                Pause();
            else
                Play();
        }

        /// <summary>
        /// Play 상태 UI 갱신
        /// </summary>
        private void RefreshPlayState()
        {
            SetPlayStatus(BaseHandler.Playing);
        }

        /// <summary>
        /// 플레이 상태 UI 설정
        /// : IConsoleHandlerUI
        /// </summary>
        public void SetPlayStatus(bool playing)
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

        #region CLI
        /// <summary>
        /// CLI 팝업 실행 버튼 클릭
        /// </summary>
        public void OnClickCLI()
        {
            ResourcePopup.Load<PopupTerminal>(PopupTerminal.ResourcePath, TransformCache)
                .Initialize().SelectInputField();
        }
        #endregion
    }
}