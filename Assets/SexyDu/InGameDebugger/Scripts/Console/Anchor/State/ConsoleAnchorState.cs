using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public abstract class ConsoleAnchorState : IConsoleAnchor
    {
        /// <summary>
        /// 앵커 타입
        /// : IConsoleAnchor
        /// </summary>
        public abstract ConsoleAnchorType Anchor { get; }

        /// <summary>
        /// 다음 앵커 반환
        /// : IConsoleAnchor
        /// </summary>
        public abstract IConsoleAnchor Next();

        /// <summary>
        /// 현재 앵커로 설정
        /// : IConsoleAnchor
        /// </summary>
        public abstract void Process(Canvas canvas, RectTransform target);

        /// <summary>
        /// 현재 orientation에 적합 여부
        /// : IConsoleAnchor
        /// </summary>
        public abstract bool Collect(ScreenOrientation orientation);

        // 앵커 시 ScreenSize 대비 화면 비율
        // 설정하려는 앵커쪽의 비율
        private const float Ratio = 0.5f;
        // 설정을 위해 반대편에 취해져야할 비율
        private const float OppositeSideRatio = 1f - Ratio; // Ratio의 Reverse 값 

        /// <summary>
        /// 앵커 반대편에 설정되어야 할 수치
        /// * 예를 들어 Top으로 앵커를 설정하려면 Whole 상태에서 반대편인 Bottom의 크기를 조절해야 하기 때문에 반대편 값 반환
        /// </summary>
        protected float GetOppositeSideValue(float value)
        {
            // 반대편을 건드려야 하기 때문에 
            return value * OppositeSideRatio;
        }

        /// <summary>
        /// 캔버스에 따른 SafeArea 반환
        /// </summary>
        protected Vector2 GetSafeAreaSize(Canvas canvas)
        {
            return Screen.safeArea.size / canvas.scaleFactor;
        }

       


    }
}