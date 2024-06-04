#if UNITY_EDITOR
using UnityEngine;

namespace SexyDu.InGameDebugger.View
{
    public partial class BugCatcher
    {
        [Header("OnlyEditor")]
        [SerializeField] private AnimationTarget animationBase;

        /// <summary>
        /// AnimationSpec 기본값 설정
        /// </summary>
        public void SetDefaultAnimationSpecs()
        {
            Color colorStart = animationTarget.GetColor();
            Color colorEnd = colorStart;
            colorEnd.a = 0f;

            Vector2 anchoredPositionStart = animationTarget.GetAnchoredPosition();
            Vector2 anchoredPositionEnd = animationBase.GetAnchoredPosition();

            for (int i = 0; i < animationSpecs.Length; i++)
            {
                animationSpecs[i]
                    .SetColorStart(colorStart)
                    .SetColorEnd(colorEnd)
                    .SetAnchoredPositionStart(anchoredPositionStart)
                    .SetAnchoredPositionEnd(anchoredPositionEnd);
            }
        }

        public partial struct AnimationTarget
        {
            /// <summary>
            /// [Editor] 현재 색상 반환
            /// </summary>
            public Color GetColor()
            {
                return graphic.color;
            }
            /// <summary>
            /// [Editor] 현재 위치 반환
            /// </summary>
            public Vector2 GetAnchoredPosition()
            {
                return rectTransform.anchoredPosition;
            }
        }

        public sealed partial class AnimationSpec
        {
            /// <summary>
            /// [Editor] 시작 색상 설정
            /// </summary>
            public AnimationSpec SetColorStart(Color color)
            {
                colorStart = color;

                return this;
            }
            /// <summary>
            /// [Editor] 종료 색상 설정
            /// </summary>
            public AnimationSpec SetColorEnd(Color color)
            {
                colorEnd = color;

                return this;
            }
            /// <summary>
            /// [Editor] 시작 위치 설정
            /// </summary>
            public AnimationSpec SetAnchoredPositionStart(Vector2 anchoredPosition)
            {
                anchoredPositionStart = anchoredPosition;

                return this;
            }
            /// <summary>
            /// [Editor] 시작 위치 설정
            /// </summary>
            public AnimationSpec SetAnchoredPositionEnd(Vector2 anchoredPosition)
            {
                anchoredPositionEnd = anchoredPosition;

                return this;
            }
        }
    }
}
#endif