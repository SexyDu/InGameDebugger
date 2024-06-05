using UnityEngine;

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
            this.stackTrace = stackTrace;
            this.type = type;
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