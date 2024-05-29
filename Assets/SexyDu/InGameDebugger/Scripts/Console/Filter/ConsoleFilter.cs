using System;

namespace SexyDu.InGameDebugger
{
    public abstract class ConsoleFilter : IConsoleFilter
    {
        /// <summary>
        /// 필터 여부 반환
        /// : IConsoleFilter
        /// </summary>
        public abstract bool IsFiltered(ILogMessage message);

        // 필터 내용 변경 시 이벤트
        private Action onChanged = null;

        /// <summary>
        /// 필터 변경 여부 옵저버 등록
        /// : IConsoleFilter
        /// </summary>
        public IConsoleFilter Subscribe(Action onChanged)
        {
            this.onChanged = onChanged;
            return this;
        }

        /// <summary>
        /// 필터 변경 알림
        /// </summary>
        protected void NotifyChanged()
        {
            onChanged?.Invoke();
        }
    }
}