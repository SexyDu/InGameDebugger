#if !UNITY_EDITOR
#define USE_CSHARP_STACKTRACE
#endif

using UnityEngine;
#if USE_CSHARP_STACKTRACE
using System.Diagnostics;
#endif

namespace SexyDu.InGameDebugger
{
    public struct TextLogMessage : ILogMessage
    {
        private readonly string condition;
        private readonly string stackTrace;
        private readonly LogType type;

        public string Condition { get { return condition; } }
        public string StackTrace { get { return stackTrace; } }
        public LogType Type { get { return type; } }

        public TextLogMessage(string condition, string stackTrace, LogType type)
        {
            this.condition = condition;
            this.type = type;
#if USE_CSHARP_STACKTRACE
            StackTrace st = new StackTrace(true);
            this.stackTrace = st.ToString();
#else
            this.stackTrace = stackTrace;
#endif
        }

        private const string WarningSign = "<color=yellow>[!]</color>";
        private const string ErrorSign = "<color=red>[!]</color>";

        public string GetLogString()
        {
            switch (Type)
            {
                case LogType.Log:
                    return string.Format($"{condition}");
                case LogType.Warning:
                    return string.Format($"{WarningSign} {condition}");
                default: //case LogType.Error: case LogType.Assert: case LogType.Exception:
                    return string.Format($"{ErrorSign} {condition}");
            }
        }
    }
}