using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class TextConsoleHandler : ConsoleHandler, ITextConsoleUIHandle
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
        // 콘솔
        [SerializeField] private TextConsole textConsole;
        protected override Console console => textConsole;
        
        #region UI
        // UI 인터페이스
        private ITextConsoleHandlerUI ui;
        protected override IConsoleHandlerUI BaseUI => ui;

        /// <summary>
        /// UI 인터페이스 연결
        /// </summary>
        public void Connect(ITextConsoleHandlerUI ui)
        {
            this.ui = ui;
        }
        #endregion

        #region StackTrace
        // StackTrace 표시 여부
        public bool EnableStackTrace => textConsole.EnableStackTrace;

        /// <summary>
        /// StackTrace 표시 여부 설정
        /// : ITextConsoleUIHandle
        /// </summary>
        public void SetStackTrace(bool enable)
        {
            textConsole.SetStackTrace(enable);

            ui?.SetStackTraceStatus(textConsole.EnableStackTrace);
        }
        #endregion
    }
}