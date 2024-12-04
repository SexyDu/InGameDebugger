using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class ConsoleAnchorWhole : ConsoleAnchorState
    {
        /// <summary>
        /// 앵커 타입
        /// : IConsoleAnchor
        /// </summary>
        public override ConsoleAnchorType Anchor => ConsoleAnchorType.Whole;

        /// <summary>
        /// 다음 앵커 반환
        /// : IConsoleAnchor
        /// </summary>
        public override IConsoleAnchor Next()
        {
            switch (Screen.orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    return new ConsoleAnchorRight();
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    return new ConsoleAnchorBottom();
                default:
                    return this;
            }
        }

        /// <summary>
        /// 현재 앵커로 설정
        /// : IConsoleAnchor
        /// </summary>
        public override void Process(RectTransform target, float scaleFactor = 1f)
        {
            target.offsetMin = Vector2.zero;
            target.offsetMax = Vector2.zero;

            //target.anchorMin = Vector2.zero;
            //target.anchorMax = Vector2.one;
        }

        /// <summary>
        /// 현재 orientation에 적합 여부
        /// : IConsoleAnchor
        /// </summary>
        public override bool Collect(ScreenOrientation orientation)
        {
            return true;
        }
    }
}