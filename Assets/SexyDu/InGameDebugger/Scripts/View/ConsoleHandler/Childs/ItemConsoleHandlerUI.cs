using UnityEngine;

namespace SexyDu.InGameDebugger.View
{
    public class ItemConsoleHandlerUI : ConsoleHandlerUI, IItemConsoleHandlerUI
    {
        [Header("Hander")]
        [SerializeField] private ItemConsoleHandler handler;
        protected override ConsoleHandler BaseHandler => handler;

        /// <summary>
        /// 초기 설정
        /// </summary>
        protected override void Initialize()
        {
            handler.Connect(this);

            base.Initialize();
        }

        /// <summary>
        /// UI 갱신
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
        }
    }
}