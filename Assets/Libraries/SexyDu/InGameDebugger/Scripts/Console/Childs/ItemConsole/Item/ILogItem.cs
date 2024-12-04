using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public interface ILogItem : IClearable
    {
        // 로그 메세지
        public ILogMessage Message { get; }

        /// <summary>
        /// 로그 메세지 설정
        /// </summary>
        public void Set(ILogMessage message);

        /// <summary>
        /// 배경색상 설정
        /// </summary>
        public ILogItem SetBackgroundColor(Color color);

        /// <summary>
        /// 활성화 상태
        /// </summary>
        public bool Activated { get; }

        /// <summary>
        /// 위치값
        /// </summary>
        public Vector2 anchoredPosition
        {
            get;
            set;
        }
    }
}