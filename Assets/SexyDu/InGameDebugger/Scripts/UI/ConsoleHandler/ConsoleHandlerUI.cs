using UnityEngine;
using UnityEngine.UI;

namespace SexyDu.InGameDebugger
{
    public abstract class ConsoleHandlerUI : MonoBehaviour
    {
        protected abstract ConsoleHandler BaseHandler { get; }

        private void RefreshUI()
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
    }
}