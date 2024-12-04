using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 콘솔 로그타입 필터 항목
    /// </summary>
    public class ConsoleFilterLogType : ConsoleFilter
    {
        public ConsoleFilterLogType(params LogType[] logTypes)
        {
            activatedLogTypes = new List<LogType>(logTypes);
        }

        public ConsoleFilterLogType(List<LogType> logTypes)
        {
            activatedLogTypes = logTypes;
        }

        /// <summary>
        /// 필터 여부 반환
        /// : IConsoleFilter
        /// </summary>
        public override bool IsFiltered(ILogMessage message)
        {
            return !activatedLogTypes.Contains(message.Type);
        }

        // 활성화 로그 타입
        private List<LogType> activatedLogTypes = null;

        /// <summary>
        /// 로그 타입 추가
        /// </summary>
        public void AddLogType(params LogType[] logTypes)
        {
            for (int i = 0; i < logTypes.Length; i++)
            {
                if (!activatedLogTypes.Contains(logTypes[i]))
                    activatedLogTypes.Add(logTypes[i]);
            }

            NotifyChanged();
        }

        /// <summary>
        /// 로그 타입 제외
        /// </summary>
        public void RemoveLogType(params LogType[] logTypes)
        {
            for (int i = 0; i < logTypes.Length; i++)
            {
                activatedLogTypes.Remove(logTypes[i]);
            }

            NotifyChanged();
        }
    }
}