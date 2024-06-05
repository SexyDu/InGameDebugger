using System.Text;
using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger
{
    public class TextConsole : Console
    {
        #region OnAwake
        [SerializeField] private bool onAwakeInit;
        [SerializeField] private bool onAwakePlay;

        private void Awake()
        {
            if (onAwakeInit)
            {
                Initialize();

                if (onAwakePlay)
                    Play();
            }
        }
        #endregion

        /// <summary>
        /// 초기 설정
        /// </summary>
        public override Console Initialize()
        {
            /// TrackTrace 크기 비율에 따른 string format 설정
            stackTraceFormat = string.Format("<size={0}>{{0}}</size>", textMesh.fontSize * stackTraceSizeRatio);

            return this;
        }

        /// <summary>
        /// 로그 클리어
        /// </summary>
        public override void Clear()
        {
            ClearLogText();

            base.Clear();
        }

        /// <summary>
        /// 로깅 메세지 이벤트
        /// </summary>
        protected override void OnLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            ILogMessage message = new TextLogMessage(condition, stackTrace, type);

            AddLogMessage(message);

            // 로그 수 수집 (for 로그 옵저버)
            CollectLogCount(type);
        }

        /// <summary>
        /// 로그 추가
        /// </summary>
        protected override void AddLogMessage(ILogMessage message)
        {
            base.AddLogMessage(message);

            AppendLogText(message);
        }

        /// <summary>
        /// 로그 화면 갱신
        /// </summary>
        public override void RefreshLogDisplay()
        {
            ClearLogText();

            for (int i = messages.Count - 1; i >= 0; i--)
            {
                if (PassFilters(messages[i]))
                {
                    InsertStringBuilder(GetLog(messages[i]));
                    if (AmendStringBuilderLength())
                        break;
                }
            }

            DisplayLogText();
        }

        #region Text
        [Header("Text")]
        [SerializeField] private TMP_Text textMesh; // 로그 텍스트 메쉬
        private int MaxLength => InGameDebuggerConfig.Ins.Settings.MaxTextLength;

        // 로그 string builder
        private StringBuilder logStringBuilder = new StringBuilder();

        /// <summary>
        /// ILogMessage를 기반으로 Log 문자열 반환
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetLog(ILogMessage message)
        {
            if (enableStackTrace)
                return string.Format("{0}\n{1}", message.GetLogString(), GetStackTrace(message));
            else
                return message.GetLogString();
        }

        /// <summary>
        /// StringBuilder Max길이 보정
        /// </summary>
        private bool AmendStringBuilderLength()
        {
            if (logStringBuilder.Length > MaxLength)
            {
                logStringBuilder.Remove(0, logStringBuilder.Length - MaxLength);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// string builder log 추가
        /// </summary>
        private void AppendStringBuilder(string log)
        {
            logStringBuilder.AppendLine();
            logStringBuilder.AppendLine(log);
        }

        /// <summary>
        /// string builder 앞에 log 추가
        /// </summary>
        public void InsertStringBuilder(string log)
        {
            logStringBuilder.Insert(0, log);
            logStringBuilder.Insert(0, "\n\n");
        }

        /// <summary>
        /// LogMessage 추가
        /// </summary>
        /// <param name="message"></param>
        private void AppendLogText(ILogMessage message)
        {
            AppendStringBuilder(GetLog(message));

            AmendStringBuilderLength();

            DisplayLogText();
        }

        /// <summary>
        /// string builder 표시
        /// </summary>
        private void DisplayLogText()
        {
            textMesh.SetText(logStringBuilder.ToString());
        }

        /// <summary>
        /// 로그 클리어
        /// </summary>
        private void ClearLogText()
        {
            logStringBuilder.Clear();

            DisplayLogText();
        }
        #endregion

        #region StackTrace
        [Header("StackTrace")]
        // 로그 크기 대비 StackTrace 크기 비율
        [SerializeField] private float stackTraceSizeRatio;
        // StackTrace 표시 여부
        private bool enableStackTrace = false;
        public bool EnableStackTrace => enableStackTrace;

        private string stackTraceFormat = "{0}";

        /// <summary>
        /// StackTrace 표시 여부 설정
        /// </summary>
        public void SetStackTrace(bool enable)
        {
            enableStackTrace = enable;

            RefreshLogDisplay();
        }

        /// <summary>
        /// TextMesh에 표시될 StackTrace 문자열
        /// </summary>
        private string GetStackTrace(ILogMessage message)
        {
            return string.Format(stackTraceFormat, message.StackTrace);
        }
        #endregion
    }
}