using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class DebuggerHome : MonoBehaviour, IConsoleActivationObserver, IActivationConfigurable
    {
        public DebuggerHome Initialize(IActivable consoleActivable)
        {
            this.consoleActivable = consoleActivable;

            return this;
        }

        private IActivable consoleActivable = null;

        public void OnClickActivateConsole()
        {
            consoleActivable.Activate();
        }

        #region IConsoleActivationObserver
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