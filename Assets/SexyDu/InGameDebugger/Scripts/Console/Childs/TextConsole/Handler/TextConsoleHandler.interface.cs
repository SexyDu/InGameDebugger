using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public interface ITextConsoleUIHandle : IConsoleUIHandle
    {
        /// <summary>
        /// StackTrace 표시 여부 설정
        /// </summary>
        public void SetStackTrace(bool enable);
    }
}

