using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 디버거 루트
    /// </summary>
    public class Debugger : MonoBehaviour, IDestroyable, IClearable
    {
        [SerializeField] private bool onAwakeInit;
        private void Awake()
        {
            if (onAwakeInit)
                Initialize();
        }

        /// <summary>
        /// 초기 설정
        /// </summary>
        public Debugger Initialize()
        {
            InGameDebuggerConfig.Ins.Debugger = this;

            // DebuggerInitializer가 있는 경우 초기화
            GetComponent<DebuggerInitializer>()?.Initialize().Release();

            consoleHandler.Initialize();

            consoleHandler.Subscribe(home.Initialize().ConsoleActivationObserver);

            return this;
        }

        /// <summary>
        /// 클리어
        /// </summary>
        public void Clear()
        {
            home.Clear();
            consoleHandler.Clear();
        }

        /// <summary>
        /// 파괴
        /// </summary>
        public void Destroy()
        {
            Clear();

            GameObject.Destroy(gameObject);
        }

        #region Home
        [Header("Home")]
        [SerializeField] private DebuggerHome home; // 홈화면
        #endregion

        #region ConsoleHandler
        [Header("ConsoleHandler")]
        [SerializeField] private ConsoleHandler consoleHandler; // 콘솔 핸들러
        public IConsoleLogSubject ConsoleLogSubject => consoleHandler.ConsoleLogSubject;
        public IConsoleActivationSubject ConsoleActivationSubject => consoleHandler;
        public IConsoleFilterFollower ConsoleFilterFollower => consoleHandler.FilterFollower;

        /// <summary>
        /// 콘솔 활성화
        /// </summary>
        public void ActivateConsole()
        {
            consoleHandler.Activate();
        }
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private Transform transformCache;
        public Transform TransformCache => transformCache;
        #endregion
    }
}