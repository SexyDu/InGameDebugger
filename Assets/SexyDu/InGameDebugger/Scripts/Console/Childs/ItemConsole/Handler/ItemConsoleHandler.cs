using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ItemConsoleHandler : ConsoleHandler, IItemConsoleUIHandle
    {
        public override ConsoleHandler Initialize()
        {
            base.Initialize();

            return this;
        }

        [Header("ItemConsole")]
        [SerializeField] private ItemConsole itemConsole;
        protected override Console console => itemConsole;

        #region UI
        private IItemConsoleHandlerUI ui;
        protected override IConsoleHandlerUI BaseUI => ui;

        public void Connect(IItemConsoleHandlerUI ui)
        {
            this.ui = ui;
        }
        #endregion
    }
}