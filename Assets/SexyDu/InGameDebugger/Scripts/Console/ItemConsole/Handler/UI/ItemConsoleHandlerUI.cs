using UnityEngine;
using UnityEngine.UI;

namespace SexyDu.InGameDebugger
{
    public class ItemConsoleHandlerUI : MonoBehaviour
    {
        private IItemConsoleUIHandle handle = null;

        public ItemConsoleHandlerUI Initialize(IItemConsoleUIHandle handle)
        {
            this.handle = handle;

            RefreshUI();

            return this;
        }

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
            handle.PauseConsole(false);

            RefreshPlayState();
        }

        private void Pause()
        {
            handle.PauseConsole(true);

            RefreshPlayState();
        }

        public void OnClickTogglePlay()
        {
            if (handle.Playing)
                Pause();
            else
                Play();
        }

        private void RefreshPlayState()
        {
            SetPlayState(handle.Playing);
        }

        private void SetPlayState(bool playing)
        {
            imagePlayState.sprite = playing ? spritePause : spritePlay;
        }
        #endregion

        #region Clear
        public void OnClickClearLog()
        {
            handle.ClearConsole();
        }
        #endregion

        #region Minimize
        public void OnClickMinimize()
        {
            handle.Inactivate();
        }
        #endregion
    }
}