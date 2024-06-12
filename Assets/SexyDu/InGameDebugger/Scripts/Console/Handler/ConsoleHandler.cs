using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 콘솔 Handler
    /// </summary>
    public abstract partial class ConsoleHandler : MonoBehaviour, IConsoleUIHandle, IConsoleAnchorSetter, IActivable, IInactivable, IClearable
    {
        // 콘솔
        protected abstract Console console { get; }
        // 로그 수집 서브젝트
        public IConsoleLogSubject ConsoleLogSubject { get => console; }
        // 콘솔 필터 팔로워 : IConsoleUIHandle
        public IConsoleFilterFollower FilterFollower { get => console; }
        // 로그 수집 상태 : IConsoleUIHandle
        public bool Playing { get { return console.Playing; } }

        /// <summary>
        /// 초기 설정
        /// </summary>
        public virtual ConsoleHandler Initialize()
        {
            console.Initialize();
            console.Play();

            return this;
        }

        /// <summary>
        /// 클리어
        /// : IClearable
        /// </summary>
        public virtual void Clear()
        {
            console.Clear();
        }

        /// <summary>
        /// 콘솔 로깅 중지 설정
        /// : IConsoleUIHandle
        /// </summary>
        public void PauseConsole(bool pause)
        {
            if (pause)
                console.Pause();
            else
                console.Play();

            BaseUI?.SetPlayStatus(Playing);
        }

        /// <summary>
        /// 콘솔 로그 클리어
        /// : IConsoleUIHandle
        /// </summary>
        public void ClearConsole()
        {
            console.Clear();
        }

        #region Anchor
        /// <summary>
        /// 현재 설정된 앵커 (초기 전체 모드 앵커)
        /// </summary>
        private IConsoleAnchor anchor = new ConsoleAnchorWhole();
        // 현재 앵커 타입 : IConsoleAnchorSetter
        public ConsoleAnchorType AnchorType => anchor.Anchor;

        /// <summary>
        /// 앵커 설정
        /// : IConsoleAnchorSetter
        /// </summary>
        public virtual void SetAnchor(IConsoleAnchor anchor)
        {
            if (anchor != null)
            {
                this.anchor = anchor;
                this.anchor.Process(rectTransformCache, InGameDebuggerConfig.Ins.Debugger.ScaleFactor);
            }
            else
                Debug.LogError("설정하려는 anchor가 null입니다.");
        }

        /// <summary>
        /// 다음 앵커 설정
        /// : IConsoleAnchorSetter
        /// </summary>
        public void NextAnchor()
        {
            SetAnchor(anchor.Next());
        }
        #endregion

        #region Activation
        /// <summary>
        /// 활성화
        /// : IActivable
        /// </summary>
        public void Activate()
        {
            SetActive(true);

            NotifyActivation();
        }

        /// <summary>
        /// 비활성화
        /// : IConsoleUIHandle
        /// </summary>
        public void Inactivate()
        {
            SetActive(false);

            NotifyActivation();
        }
        #endregion

        #region UI
        // 베이스 UI 인터페이스
        protected abstract IConsoleHandlerUI BaseUI { get; }
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private GameObject gameObjectCache;
        private GameObject GameObjectCache { get => gameObjectCache; }
        [SerializeField] private RectTransform rectTransformCache;
        private RectTransform RectTransformCache { get => rectTransformCache; }

        private void SetActive(bool active)
        {
            GameObjectCache.SetActive(active);

            console.SetDisplayStatus(active);
        }
        #endregion
    }
}