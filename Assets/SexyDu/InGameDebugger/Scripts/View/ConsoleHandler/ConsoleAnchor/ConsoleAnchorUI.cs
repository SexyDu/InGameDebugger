using System;
using UnityEngine;
using SexyDu.UI.UGUI;

namespace SexyDu.InGameDebugger.View
{
    /// <summary>
    /// 앵커 UI
    /// TODO: 현재는 Anchor 기능이 독립적으로 구성되어 있음. ConsoleHandler에 앵커 기능에 대한 의존성을 부여하여 여기에선 해당 기능을 제어만 해야함
    ///  - 위 업무를 하면 Handler에서 Anchor변경이 제대로 감지되고 Anchor 변경 시 필요한 작업 가능 (예를들어 Anchor 변경으로 인한 슬라이더 영역 변경 등?)
    ///    - 현재는 위와 같은 연결성을 두려면 구조가 복잡해지기 때문에 하지 않음
    /// </summary>
    public class ConsoleAnchorUI : MonoBehaviour
    {
        private IConsoleAnchor current = null;
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform target;

        /// <summary>
        /// 초기 설정
        /// </summary>
        /// <returns></returns>
        public ConsoleAnchorUI Initialize()
        {
            current = new ConsoleAnchorWhole();

            return this;
        }

        /// <summary>
        /// 다음 앵커 설정
        /// </summary>
        private void Next()
        {
            current = current.Next();

            current.Process(canvas, target);

            SetCurrentIconImage();
        }

        /// <summary>
        /// 다음 앵커 클릭
        /// </summary>
        public void OnClickNext()
        {
            Next();
        }

        #region Icon
        [Header("Icon")]
        [SerializeField] private NullableImage icon; // 아이콘 이미지
        [SerializeField] private AnchorSprite[] iconSprites; // 앵커에 따른 아이콘 Sprite 구조체

        /// <summary>
        /// 현재 아이콘 이미지 설정
        /// </summary>
        private void SetCurrentIconImage()
        {
            SetIconImage(current.Anchor);
        }

        /// <summary>
        /// 앵커에 따른 아이콘 이미지 설정
        /// </summary>
        private void SetIconImage(ConsoleAnchorType anchor)
        {
            for (int i = 0; i < iconSprites.Length; i++)
            {
                if (iconSprites[i].Correct(anchor))
                {
                    icon.SetSprite(iconSprites[i].Sprite);
                    return;
                }
            }

            icon.SetSprite(null);
        }

        /// <summary>
        /// 앵커에 따른 Sprite 구조체
        /// </summary>
        [Serializable]
        public struct AnchorSprite
        {
            [SerializeField] private ConsoleAnchorType anchor;
            [SerializeField] private Sprite sprite;

            public Sprite Sprite => sprite;

            public bool Correct(ConsoleAnchorType anchor)
            {
                return this.anchor.Equals(anchor);
            }
        }
        #endregion
    }
}