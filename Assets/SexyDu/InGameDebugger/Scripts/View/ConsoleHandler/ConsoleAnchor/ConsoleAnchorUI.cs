using System;
using UnityEngine;
using SexyDu.UI.UGUI;

namespace SexyDu.InGameDebugger.View
{
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