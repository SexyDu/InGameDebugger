using UnityEngine;
using SexyDu.UGUI;

namespace SexyDu.InGameDebugger.UI
{
    public class ItemConsoleHandlerUI : ConsoleHandlerUI, IItemConsoleHandlerUI
    {
        [Header("Hander")]
        [SerializeField] private ItemConsoleHandler handler;
        protected override ConsoleHandler BaseHandler => handler;

        protected override void Initialize()
        {
            handler.Connect(this);

            base.Initialize();
        }

        public override void Refresh()
        {
            base.Refresh();
        }

        #region CLI
        [Header("CLI")]
        [SerializeField] private Transform parentOfCLI; // [임시] 테스트용으로만 쓰자
        public void OnClickCLI()
        {
            ResourcePopup.Load<PopupTerminal>(PopupTerminal.ResourcePath, parentOfCLI)
                .Initialize().SelectInputField();
        }
        #endregion
    }
}