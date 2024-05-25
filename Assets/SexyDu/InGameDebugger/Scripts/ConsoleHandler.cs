using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ConsoleHandler : MonoBehaviour
    {
        // 콘솔
        [SerializeField] private Console console;
        // 로그 수집 서브젝트
        public IConsoleLogSubject ConsoleLogSubject { get => console; }

        /// <summary>
        /// 초기 설정
        /// </summary>
        public ConsoleHandler Initialize()
        {
            console.Initialize();
            console.Play();

            return this;
        }

        /// <summary>
        /// 콘솔 로깅 중지 설정
        /// </summary>
        public void PauseConsole(bool pause)
        {
            if (pause)
                console.Pause();
            else
                console.Play();
        }

        #region ObjectCache
        [SerializeField] private GameObject gameObjectCache;
        private GameObject GameObjectCache { get => gameObjectCache; }

        public void SetActive(bool active)
        {
            GameObjectCache.SetActive(active);
        }
        #endregion
    }
}