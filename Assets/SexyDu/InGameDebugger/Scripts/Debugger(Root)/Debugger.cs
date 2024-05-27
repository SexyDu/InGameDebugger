using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private bool onAwakeInit;
        private void Awake()
        {
            if (onAwakeInit)
                Initialize();
        }

        private Debugger Initialize()
        {
            InGameDebuggerConfig.Ins.Debugger = this;

            consoleHandler.Initialize();

            consoleHandler.Subscribe(home.Initialize(consoleHandler));

            return this;
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

        private void OnConsoleActivationChanged(bool active)
        {
            SetHomeActive(!active);
        }

        #region Home
        [SerializeField] private DebuggerHome home;

        private void SetHomeActive(bool active)
        {
            home.SetActive(active);
        }
        #endregion

        #region ConsoleHandler
        [Header("ConsoleHandler")]
        [SerializeField] private ConsoleHandler consoleHandler;
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private Transform transformCache;
        public Transform TransformCache { get => transformCache; }
        #endregion
    }
}