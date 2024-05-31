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

        /// <summary>
        /// 초기 설정
        /// </summary>
        private Debugger Initialize()
        {
            InGameDebuggerConfig.Ins.Debugger = this;

            GetComponent<DebuggerInitializer>()?.Initialize().Release();

            consoleHandler.Initialize();

            consoleHandler.Subscribe(home.Initialize(consoleHandler));

            return this;
        }

        /// <summary>
        /// 클리어
        /// </summary>
        private void Clear()
        {
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
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private Transform transformCache;
        public Transform TransformCache { get => transformCache; }
        #endregion
    }
}