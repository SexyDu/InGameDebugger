using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ConsoleAnchorLeft : ConsoleAnchorLandscapeState
    {
        /// <summary>
        /// 앵커 타입
        /// : IConsoleAnchor
        /// </summary>
        public override ConsoleAnchorType Anchor => ConsoleAnchorType.Left;

        /// <summary>
        /// 다음 앵커 반환
        /// : IConsoleAnchor
        /// </summary>
        public override IConsoleAnchor Next()
        {
            return new ConsoleAnchorWhole();
        }

        /// <summary>
        /// 현재 앵커로 설정
        /// : IConsoleAnchor
        /// </summary>
        public override void Process(Canvas canvas, RectTransform target)
        {
            Vector2 canvasSafeArea = GetSafeAreaSize(canvas);

            target.offsetMin = Vector2.zero;
            target.offsetMax = new Vector3(-GetOppositeSideValue(canvasSafeArea.x), 0f);

            //target.anchorMin = Vector2.zero;
            //target.anchorMax = Vector2.one;
        }
    }
}