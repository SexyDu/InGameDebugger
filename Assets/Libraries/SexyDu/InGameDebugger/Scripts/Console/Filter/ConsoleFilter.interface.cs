using System;

namespace SexyDu.InGameDebugger
{
    public interface IConsoleFilter
    {
        /// <summary>
        /// 필터 여부 반환
        /// </summary>
        public bool IsFiltered(ILogMessage message);

        /// <summary>
        /// 필터 변경 여부 옵저버 등록
        /// </summary>
        public IConsoleFilter Subscribe(Action onChanged);
    }

    public interface IConsoleFilterFollower
    {
        public void FollowFilter(IConsoleFilter filter);

        public void UnfollowFilter(IConsoleFilter filter);
    }
}