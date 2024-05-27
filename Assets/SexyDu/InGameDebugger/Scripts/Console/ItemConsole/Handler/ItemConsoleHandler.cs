using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ItemConsoleHandler : ConsoleHandler, IItemConsoleUIHandle
    {
        public override ConsoleHandler Initialize()
        {
            base.Initialize();

            ui.Initialize(this);

            return this;
        }

        [Header("ItemConsole")]
        [SerializeField] private ItemConsole itemConsole;
        protected override Console console => itemConsole;

        #region UI
        [Header("UI")]
        [SerializeField] private ItemConsoleHandlerUI ui;
        #endregion
    }
}