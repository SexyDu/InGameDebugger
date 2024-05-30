#if UNITY_EDITOR
#define ONLY_EDITOR
#endif

#if ONLY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SexyDu.InGameDebugger.ForEditor
{
    public class LogItemForEditor : MonoBehaviour
    {
        [SerializeField] private Image background; // 배경 이미지
        [SerializeField] private Image icon; // 아이콘 이미지
        [SerializeField] private TMP_Text text; // 텍스트
        [SerializeField] private RectTransform rectTransformCache; // RectTransform
        [SerializeField] private GameObject gameObjectCache; // GameObject

        public LogItemForEditor SetBackground(Image background)
        {
            this.background = background;

            return this;
        }

        public LogItemForEditor SetIcon(Image icon)
        {
            this.icon = icon;

            return this;
        }

        public LogItemForEditor SetTextMesh(TMP_Text text)
        {
            this.text = text;

            return this;
        }

        public LogItemForEditor SetRectTransform(RectTransform rectTransformCache)
        {
            this.rectTransformCache = rectTransformCache;

            return this;
        }

        public LogItemForEditor SetGameObject(GameObject gameObjectCache)
        {
            this.gameObjectCache = gameObjectCache;

            return this;
        }
    }
}
#endif