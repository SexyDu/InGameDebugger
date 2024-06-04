using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class DebuggerHome : MonoBehaviour, IDebuggerHome, IConsoleActivationObserver, IActivationConfigurable, IClearable
    {
        /// <summary>
        /// 초기 설정
        /// </summary>
        /// <param name="consoleActivable"></param>
        /// <returns></returns>
        public IDebuggerHome Initialize()
        {
            for (int i = 0; i < inHomeContents.Length; i++)
            {
                inHomeContents[i].Initialize();
            }

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
            Debug.LogFormat("콘솔 활성화 상태 변경 : {0}", active);
            SetActive(!active);
        }
        #endregion

        #region MonoInDebuggerHome
        [SerializeField] private MonoInDebuggerHome[] inHomeContents;
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