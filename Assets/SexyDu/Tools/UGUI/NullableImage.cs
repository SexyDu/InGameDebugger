using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SexyDu.UGUI
{
    [Serializable]
    public struct NullableImage
    {
        [SerializeField] private Image image;
        public Image Image { get => image; }

        public Sprite sprite
        {
            set
            {
                image.sprite = value;

                SetColor();
            }
        }

        private void SetColor()
        {
            image.color = image.sprite == null ? Color.clear : Color.white;
        }
    }
}