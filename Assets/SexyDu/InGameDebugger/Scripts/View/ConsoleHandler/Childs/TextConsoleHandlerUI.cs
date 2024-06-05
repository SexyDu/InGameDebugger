using UnityEngine;

namespace SexyDu.InGameDebugger.View
{
    public class TextConsoleHandlerUI : ConsoleHandlerUI, ITextConsoleHandlerUI
    {
        [Header("Hander")]
        [SerializeField] private TextConsoleHandler handler;
        protected override ConsoleHandler BaseHandler => handler;

        /// <summary>
        /// 초기 설정
        /// </summary>
        protected override void Initialize()
        {
            handler.Connect(this);

            SetStackTraceStatus(handler.EnableStackTrace);

            base.Initialize();
        }

        /// <summary>
        /// UI 갱신
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
        }

        #region StackTrace
        [Header("StackTrace")]
        [SerializeField] private GameObject stackTraceBlockSign;

        /// <summary>
        /// StackTrace 상태 UI 설정
        /// : ITextConsoleHandlerUI
        /// </summary>
        public void SetStackTraceStatus(bool enable)
        {
            stackTraceBlockSign.SetActive(!enable);
        }

        /// <summary>
        /// StackTrace 상태 토글 클릭
        /// </summary>
        public void OnClickToogleStackTrace()
        {
            handler.SetStackTrace(!handler.EnableStackTrace);
        }
        #endregion

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0f, 0f, 100f, 100f), ""))
            {
                OnClickToogleStackTrace();
            }
        }
    }
}