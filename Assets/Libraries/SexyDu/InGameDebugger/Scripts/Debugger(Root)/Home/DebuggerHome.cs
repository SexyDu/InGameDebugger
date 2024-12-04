using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger
{
    public class DebuggerHome : MonoBehaviour, IDebuggerHome, IConsoleActivationObserver, IActivationConfigurable, IClearable, IFramerateObserver
    {
        /// <summary>
        /// 초기 설정
        /// </summary>
        public IDebuggerHome Initialize(Debugger debugger)
        {
            for (int i = 0; i < inHomeContents.Length; i++)
            {
                inHomeContents[i].Initialize();
            }

            // 터치 인터페이스로 콘솔 활성화 기능 설정 및 디버거 연결
            activator = InGameDebuggerConfig.Ins.Settings.GetActivator();
            debugger.ConnectToConsoleActivator(activator);
            activator.SetEnableActivation(true);

            // 프레임 레이트 서브젝트 생성 및 옵저버 등록
            framerateSubject = new FramerateSubject(0.5f);
            framerateSubject.Subscribe(this);

            return this;
        }

        /// <summary>
        /// 클리어
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < inHomeContents.Length; i++)
            {
                inHomeContents[i].Clear();
            }

            activator.Release();
            activator = null;

            framerateSubject?.Dispose();
            framerateSubject = null;
        }

        public IConsoleActivationObserver ConsoleActivationObserver => this;

        #region IConsoleActivationObserver
        /// <summary>
        /// 콘솔 활성화 옵저버 이벤트
        /// : IConsoleActivationObserver
        /// </summary>
        /// <param name="active"></param>
        public void OnConsoleActivationChanged(bool active)
        {
            bool activaHome = !active;

            SetActive(activaHome);
            activator?.SetEnableActivation(activaHome);
        }
        #endregion

        #region ConsoleActivator
        // 콘솔 활성화 기능
        private IConsoleActivator activator = null;
        #endregion

        #region MonoInDebuggerHome
        [SerializeField] private MonoInDebuggerHome[] inHomeContents;
        #endregion

        #region Framerate
        [Header("Framerate")]
        [SerializeField] private TMP_Text framerateText;
        private IFramerateSubject framerateSubject = null;
        
        public void OnReceivedFrameRate(float framerate)
        {
            framerateText.text = framerate.ToString("F2");
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