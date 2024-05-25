using System;
using UnityEngine;

namespace SexyDu
{
    namespace InGameDebugger
    {
        public interface ILogMessage
        {
            public string Condition { get; }
            public string StackTrace { get; }
            public LogType Type { get; }

            public string GetLogString();
        }

        public struct LogMessage : ILogMessage
        {
            private readonly string condition;
            private readonly string stackTrace;
            private readonly LogType type;
            private readonly string time;

            public string Condition { get { return condition; } }
            public string StackTrace { get { return stackTrace; } }
            public LogType Type { get { return type; } }

            public LogMessage(string condition, string stackTrace, LogType type)
            {
                this.condition = condition;
                this.stackTrace = stackTrace;
                this.type = type;

                this.time = DateTime.Now.ToString("HH:mm:ss");
            }

            public string GetLogString()
            {
                return string.Format($"[{time}] {condition}\n{stackTrace}");
            }
        }

        public struct LogMessageTest : ILogMessage
        {
            private readonly string condition;
            private readonly string stackTrace;
            private readonly LogType type;

            public string Condition { get { return condition; } }
            public string StackTrace { get { return stackTrace; } }
            public LogType Type { get { return type; } }

            public LogMessageTest(string condition, string stackTrace, LogType type, int index)
            {
                this.condition = condition;
                this.stackTrace = stackTrace;
                this.type = type;

                this.index = index;
            }

            #region Index
            private int index;

            public int Index { get { return index; } }
            #endregion

            public string GetLogString()
            {
                return string.Format(GetLogStringFormat(type), index, condition);
            }

            private string GetLogStringFormat(LogType type)
            {
                switch (type)
                {
                    case LogType.Log:
                        return "[{0}] {1}";
                    case LogType.Warning:
                        return "<color=#ffff00>[{0}]</color> {1}";
                    default: // case LogType.Error: case LogType.Assert: case LogType.Exception:
                        return "<color=#ff0000>[{0}]</color> {1}";
                }
            }
        }
    }
}