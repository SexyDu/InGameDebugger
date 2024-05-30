using UnityEngine;

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
    }
}