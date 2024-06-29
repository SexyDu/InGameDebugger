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
            DebuggerInitializer initializer = GetComponent<DebuggerInitializer>();

            // initializer가 있는 경우 (아직 초기 설정 안된 상태)
            if (initializer != null)
            {
                // 기본 초기 설정 (후 Component 해제)
                initializer.Initialize(canvas).Release();

                // 콘솔 핸들러 초기설정
                consoleHandler.Initialize();

                // 홈 초기 설정과 동시에 콘솔 활성화 상태 옵저버 등록
                consoleHandler.Subscribe(home.Initialize(this).ConsoleActivationObserver);

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("이미 초기화 되었습니다. 선 넘지 마시죠?");
            }

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

        /// <summary>
        /// 콘솔 활성화 인터페이스에 콘솔 Activable 연결
        /// </summary>
        public void ConnectToConsoleActivator(IActivator activator)
        {
            activator.Set(consoleHandler);
        }
        #endregion

        #region Canvas
        [Header("Canvas")]
        [SerializeField] private Canvas canvas; // 캔버스
        //public Canvas Canvas => canvas;
        public float ScaleFactor => canvas.scaleFactor;
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private Transform transformCache;
        public Transform TransformCache => transformCache;
        #endregion
    }
}