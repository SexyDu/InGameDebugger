using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public interface IConsoleAnchor
    {
        /// <summary>
        /// 앵커 타입
        /// </summary>
        public ConsoleAnchorType Anchor { get; }

        /// <summary>
        /// 다음 앵커 반환
        /// </summary>
        public IConsoleAnchor Next();

        /// <summary>
        /// 현재 앵커로 설정
        /// </summary>
        public void Process(Canvas canvas, RectTransform target);

        /// <summary>
        /// 현재 orientation에 적합 여부
        /// </summary>
        public bool Collect(ScreenOrientation orientation);
    }

    public enum ConsoleAnchorType : byte
    {
        Whole = 0,
        Bottom,
        Top,
        Right,
        Left,
    }
}