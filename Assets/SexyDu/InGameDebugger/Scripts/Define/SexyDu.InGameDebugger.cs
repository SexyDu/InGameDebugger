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

        [Serializable]
        public enum DebuggerType : byte
        {
            Unknown = 0,
            Item,
            Text,
        }
    }
}