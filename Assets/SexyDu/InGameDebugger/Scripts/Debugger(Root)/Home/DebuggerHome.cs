using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class DebuggerHome : MonoBehaviour, IDebuggerHome, IConsoleActivationObserver, IActivationConfigurable
    {
        public IDebuggerHome Initialize(IActivable consoleActivable)
        {
            this.consoleActivable = consoleActivable;

            return this;
        }

        public IConsoleActivationObserver ConsoleActivationObserver => this;

        // 콘솔 활성화 인터페이스
        private IActivable consoleActivable = null;

        /// <summary>
        /// 콘솔 활성화 클릭
        /// </summary>
        public void OnClickActivateConsole()
        {
            consoleActivable.Activate();
        }

        #region IConsoleActivationObserver
        /// <summary>
        /// 콘솔 활성화 옵저버 이벤트
        /// : IConsoleActivationObserver
        /// </summary>
        /// <param name="active"></param>
        public void OnConsoleActivationChanged(bool active)
        {
            Debug.LogFormat("콘솔 활성화 상태 변경 : {0}", active);
            SetActive(!active);
        }
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private GameObject gameObjectCache;
        private GameObject GameObjectCache { get => gameObjectCache; }

        public void SetActive(bool active)
        {
            GameObjectCache.SetActive(active);
        }
        #endregion
    }
}