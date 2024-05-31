using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ConsoleAnchorBottom : ConsoleAnchorPortraitState
    {
        /// <summary>
        /// 앵커 타입
        /// : IConsoleAnchor
        /// </summary>
        public override ConsoleAnchorType Anchor => ConsoleAnchorType.Bottom;

        /// <summary>
        /// 다음 앵커 반환
        /// : IConsoleAnchor
        /// </summary>
        public override IConsoleAnchor Next()
        {
            return new ConsoleAnchorTop();
        }

        /// <summary>
        /// 현재 앵커로 설정
        /// : IConsoleAnchor
        /// </summary>
        public override void Process(Canvas canvas, RectTransform target)
        {
            Vector2 canvasSafeArea = GetSafeAreaSize(canvas);

            target.offsetMin = Vector2.zero;
            target.offsetMax = new Vector3(0f, -GetOppositeSideValue(canvasSafeArea.y));

            //target.anchorMin = Vector2.zero;
            //target.anchorMax = Vector2.one;
        }
    }
}