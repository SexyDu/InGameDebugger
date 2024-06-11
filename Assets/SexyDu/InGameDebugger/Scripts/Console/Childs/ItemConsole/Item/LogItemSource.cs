#define USE_READONLY

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SexyDu.UI.UGUI;
using SexyDu.Touch;

namespace SexyDu.InGameDebugger
{
    public class LogItemSource : MonoBehaviour
    {
        public const string ResourcePath = "IGDPrefabs/LogItemSource";

        [SerializeField] private Image background; // 배경 이미지
        [SerializeField] private Image icon; // 아이콘 이미지
        [SerializeField] private TMP_Text text; // 텍스트

        [SerializeField] private ButtonInTouchSender button; // 버튼 오브젝트

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private RectTransform rectTransformCache;
        [SerializeField] private GameObject gameObjectCache;
        #endregion

        /// <summary>
        /// 해당 클래스를 LogItem으로 컨버팅
        /// </summary>
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

        /// <summary>
        /// 해당 클래스를 LogItem으로 컨버팅
        /// * 버튼에 TouchReceiver 연결하여 슬라이더 대응
        /// </summary>
        public LogItem ConvertLogItem(ITouchTarget touchReceiver)
        {
            button.SetTouchReceiver(touchReceiver);

            return ConvertLogItem();
        }
    }
}