using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 로그 수집에 따른 옵저버패턴
    /// </summary>
    public abstract partial class Console : IConsoleFilterFollower
    {
        // 콘솔 필터 리스트
        private List<IConsoleFilter> filters = new List<IConsoleFilter>();

        /// <summary>
        /// 메세지의 필터 통과 여부
        /// </summary>
        protected bool PassFilters(ILogMessage message)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                if (filters[i].IsFiltered(message))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 필터가 변경될 시 이벤트
        /// </summary>
        private void OnChangedFilter()
        {
            // 로그 화면 갱신
            RefreshLogDisplay();
        }

        /// <summary>
        /// 필터 추가
        /// </summary>
        /// <param name="filter"></param>
        public void FollowFilter(IConsoleFilter filter)
        {
            // 전달받은 필터가 리스트에 없는 경우
            if (!filters.Contains(filter))
                // 필터 상태 변경 옵저거 등록과 동시에 리스트에 추가
                filters.Add(filter.Subscribe(OnChangedFilter));
        }

        /// <summary>
        /// 필터 제거
        /// </summary>
        public void UnfollowFilter(IConsoleFilter filter)
        {
            filters.Remove(filter);
        }
    }
}
