#define USE_READONLY

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SexyDu.InGameDebugger
{
    public class LogItemSource : MonoBehaviour
    {
        public const string ResourcePath = "IGDPrefabs/LogItemSource";

        [SerializeField] private Image background; // 배경 이미지
        [SerializeField] private Image icon; // 아이콘 이미지
        [SerializeField] private TMP_Text text; // 텍스트

        [SerializeField] private Button button; // 버튼 오브젝트

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private RectTransform rectTransformCache;
        [SerializeField] private GameObject gameObjectCache;
        #endregion

        public LogItem ConvertLogItem()
        {
#if USE_READONLY
            LogItem item = new LogItem(background, icon, text, rectTransformCache, gameObjectCache)
                .AddButtonListener(button.onClick);
#else
            LogItem item = new LogItem()
                .SetBackground(background)
                .Set(icon)
                .Set(text)
                .Set(rectTransformCache)
                .Set(gameObjectCache)
                .AddButtonListener(button.onClick);
#endif

            Destroy(this);

            return item;
        }
    }
}