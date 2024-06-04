using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SexyDu.Tool;

namespace SexyDu.InGameDebugger.View
{
    public partial class BugCatcher
    {
        [Header("Animation")]
        [SerializeField] private AnimationTarget animationTarget; // 애니메이션 대상
        [SerializeField] private AnimationSpec[] animationSpecs; // 애니메이션 정보
        [SerializeField] private float animationTime; // 애니메이션 시간
        private float animationTimeForMult; // 나눗셈을 곱셈으로 연산하기 위한 변수
        private IDisposable coroutine = null;

        /// <summary>
        /// 애니메이션 초기 설정
        /// </summary>
        private void InitializeAnimation()
        {
            for (int i = 0; i < animationSpecs.Length; i++)
            {
                animationSpecs[i].Initialize();
            }

            animationTimeForMult = 1f / animationTime;
        }

        /// <summary>
        /// LogType에 따른 애니메이션 정보 반환
        /// </summary>
        private AnimationSpec GetAnimationSpec(LogType type)
        {
            for (int i = 0; i < animationSpecs.Length; i++)
            {
                if (animationSpecs[i].Equals(type))
                    return animationSpecs[i];
            }

            return null;
        }

        /// <summary>
        /// 애니메이션 실행
        /// </summary>
        /// <param name="type"></param>
        private void Animate(LogType type)
        {
            Debug.LogFormat("BugCatcher enabled : {0}", enabled);
            if (enabled) // 활성화 되어있는 경우만 실행
            {
                AnimationSpec spec = GetAnimationSpec(type);
                if (spec != null)
                {
                    CancelAnimation();

                    coroutine = MonoHelper.StartCoroutine(CoAnimate(spec));
                }
            }
        }

        /// <summary>
        /// 애니메이션 취소
        /// </summary>
        private void CancelAnimation()
        {
            if (coroutine != null)
            {
                coroutine.Dispose();
                coroutine = null;
            }
        }

        /// <summary>
        /// 애니메이션 코루틴
        /// </summary>
        private IEnumerator CoAnimate(AnimationSpec spec)
        {
            float startTime = Time.time;
            animationTarget.Set(spec.ColorStart, spec.AnchoredPositionStart);

            do
            {
                yield return null;

                float ratio = (Time.time - startTime) * animationTimeForMult;
                if (ratio < 1f)
                {
                    animationTarget.Set(spec.GetColor(ratio), spec.GetAnchoredPosition(ratio));
                }
                else
                {
                    animationTarget.Set(spec.ColorEnd, spec.AnchoredPositionEnd);
                    break;
                }

            } while (true);
        }

        /// <summary>
        /// 애니메이션 대상 구조
        /// </summary>
        [Serializable]
        public partial struct AnimationTarget
        {
            [SerializeField] private Graphic graphic;
            [SerializeField] private RectTransform rectTransform;

            public Color color { set { graphic.color = value; } }
            public Vector2 anchoredPosition { set { rectTransform.anchoredPosition = value; } }

            public void Set(Color color, Vector2 anchoredPosition)
            {
                this.color = color;
                this.anchoredPosition = anchoredPosition;
            }
        }

        /// <summary>
        /// 애니메이션 정보
        /// </summary>
        [Serializable]
        public sealed partial class AnimationSpec
        {
            [SerializeField] private LogType type;

            [Header("Color")]
            [SerializeField] private Color colorStart; // ratio 0의 컬러값
            [SerializeField] private Color colorEnd; // ratio 1의 컬러값
            private Color colorGap; // 색상간 차이값

            [Header("Position")]
            [SerializeField] private Vector2 anchoredPositionStart; // ratio 0의 위치값
            [SerializeField] private Vector2 anchoredPositionEnd; // ratio 1의 위치값
            private Vector2 anchoredPositionGap; // 위치간 차이값

            #region External Access
            public Color ColorStart => colorStart;
            public Color ColorEnd => colorEnd;
            public Vector2 AnchoredPositionStart => anchoredPositionStart;
            public Vector2 AnchoredPositionEnd => anchoredPositionEnd;
            #endregion

            /// <summary>
            /// 초기 설정
            /// </summary>
            public void Initialize()
            {
                colorGap = colorEnd - colorStart;
                anchoredPositionGap = anchoredPositionEnd - anchoredPositionStart;
            }

            /// <summary>
            /// 동일 LogType 여부
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public bool Equals(LogType type)
            {
                return this.type.Equals(type);
            }

            /// <summary>
            /// ratio에 따른 색상
            /// </summary>
            public Color GetColor(float ratio)
            {
                return colorStart + (colorGap * ratio);
            }
            /// <summary>
            /// ratio에 따른 위치
            /// </summary>
            public Vector2 GetAnchoredPosition(float ratio)
            {
                return anchoredPositionStart + (anchoredPositionGap * ratio);
            }
        }
    }
}