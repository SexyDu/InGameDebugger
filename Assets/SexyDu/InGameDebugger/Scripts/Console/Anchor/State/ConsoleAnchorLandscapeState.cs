using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public abstract class ConsoleAnchorLandscapeState : ConsoleAnchorState
    {
        /// <summary>
        /// 현재 orientation에 적합 여부
        /// : IConsoleAnchor
        /// </summary>
        public override bool Collect(ScreenOrientation orientation)
        {
            return
                orientation.Equals(ScreenOrientation.LandscapeLeft)
                && orientation.Equals(ScreenOrientation.LandscapeRight)
                ;
        }
    }
}