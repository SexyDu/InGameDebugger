using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ConsoleAnchorRight : ConsoleAnchorLandscapeState
    {
        /// <summary>
        /// 앵커 타입
        /// : IConsoleAnchor
        /// </summary>
        public override ConsoleAnchorType Anchor => ConsoleAnchorType.Right;

        /// <summary>
        /// 다음 앵커 반환
        /// : IConsoleAnchor
        /// </summary>
        public override IConsoleAnchor Next()
        {
            return new ConsoleAnchorLeft();
        }

        /// <summary>
        /// 현재 앵커로 설정
        /// : IConsoleAnchor
        /// </summary>
        public override void Process(RectTransform target, float scaleFactor = 1f)
        {
            Vector2 canvasSafeArea = GetSafeAreaSize(scaleFactor);

            target.offsetMin = new Vector3(GetOppositeSideValue(canvasSafeArea.x), 0f);
            target.offsetMax = Vector2.zero;

            //target.anchorMin = Vector2.zero;
            //target.anchorMax = Vector2.one;
        }
    }
}