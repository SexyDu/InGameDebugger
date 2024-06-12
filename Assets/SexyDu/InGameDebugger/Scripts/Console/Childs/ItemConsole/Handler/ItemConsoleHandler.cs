using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 아이템 콘솔 Handler
    /// </summary>
    public class ItemConsoleHandler : ConsoleHandler, IItemConsoleUIHandle
    {
        /// <summary>
        /// 초기 설정
        /// </summary>
        public override ConsoleHandler Initialize()
        {
            base.Initialize();

            return this;
        }

        [Header("Console")]
        // 아이템 콘솔
        [SerializeField] private ItemConsole itemConsole;
        protected override Console console => itemConsole;

        /// <summary>
        /// 앵커 설정
        /// : IConsoleAnchorSetter
        /// </summary>
        public override void SetAnchor(IConsoleAnchor anchor)
        {
            base.SetAnchor(anchor);

            // 앵커 변경에 따른 아이템 슬라이더 제한 수치 재설정
            itemConsole.RefreshItemSlider();
        }

        #region UI
        // UI 인터페이스
        private IItemConsoleHandlerUI ui;
        protected override IConsoleHandlerUI BaseUI => ui;

        /// <summary>
        /// UI 인터페이스 연결
        /// </summary>
        public void Connect(IItemConsoleHandlerUI ui)
        {
            this.ui = ui;
        }
        #endregion
    }
}