#if !UNITY_EDITOR
#define TEST_CSHARP_STACKTRACE
#endif

using System;
using UnityEngine;
using System.Diagnostics;

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
#if TEST_CSHARP_STACKTRACE
                StackTrace st = new StackTrace(true);
#if false
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);
                    sb.AppendFormat("{0} (line {1}, col {2} in file {3}",
                        sf.HasMethod() ? sf.GetMethod().ToString() : "[none]",
                        sf.GetFileLineNumber(), sf.GetFileColumnNumber(),
                        sf.GetFileName());
                    sb.AppendLine();
                }
                this.stackTrace = sb.ToString();
#else
                this.stackTrace = st.ToString();
#endif

#endif

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