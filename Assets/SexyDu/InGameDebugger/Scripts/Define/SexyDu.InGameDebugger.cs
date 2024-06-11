#if !UNITY_EDITOR
#define USE_CSHARP_STACKTRACE
#endif
#define HIDE_STACKTRACE_INLOG

using System;
using UnityEngine;
#if USE_CSHARP_STACKTRACE
using System.Diagnostics;
#endif

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
                this.type = type;
#if USE_CSHARP_STACKTRACE
                StackTrace st = new StackTrace(true);
                this.stackTrace = st.ToString();
#else
                this.stackTrace = stackTrace;
#endif

                this.time = DateTime.Now.ToString("HH:mm:ss");
            }

            public string GetLogString()
            {
#if HIDE_STACKTRACE_INLOG
                return string.Format($"[{time}] {condition}");
#else
                return string.Format($"[{time}] {condition}\n{stackTrace}");
#endif
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