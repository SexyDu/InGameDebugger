using System;
using UnityEngine;
using TMPro;
using SexyDu.UGUI;

namespace SexyDu.InGameConsole
{
    /// <summary>
    /// 로그 상세 보기
    /// </summary>
    public class LogDetail : MonoBehaviour
    {
        public const string ResourcesPath = "IGCPrefabs/LogDetail";

        [SerializeField] private NullableImage icon;
        [SerializeField] private TMP_Text text;

        private Action onClosed = null; // 종료 시 이벤트

        /// <summary>
        /// 초기 설정
        /// </summary>
        public LogDetail Initialize()
        {
            RectTransformCache.offsetMin = Vector2.zero;
            RectTransformCache.offsetMax = Vector2.zero;

            return this;
        }
        /// <summary>
        /// 메세지 설정
        /// </summary>
        public LogDetail Set(ILogMessage message)
        {
            icon.sprite = InGameConsoleConfig.Ins.GetLogIcon(message.Type);
            text.SetText(message.GetLogString());

            return this;
        }
        /// <summary>
        /// 종료 시 이벤트 콜백 설정
        /// </summary>
        public LogDetail CallbackOnClose(Action onClosed)
        {
            this.onClosed = onClosed;

            return this;
        }

        /// <summary>
        /// 클리어
        /// </summary>
        private void Clear()
        {
            icon.sprite = null;
            text.SetText(string.Empty);
        }
        /// <summary>
        /// 파괴
        /// </summary>
        private void Destroy()
        {
            Destroy(gameObject);
        }
        /// <summary>
        /// 종료 클릭 이벤트
        /// </summary>
        public void OnClickClose()
        {
            Destroy();

            if (onClosed != null)
                onClosed();
        }
        /// <summary>
        /// 로그 복사 이벤트
        /// </summary>
        public void OnClickCopy()
        {
            GUIUtility.systemCopyBuffer = text.text;
        }

        #region ObjectCaches
        [Header("ObjectCaches")]
        [SerializeField] private RectTransform rectTransformCache;
        private RectTransform RectTransformCache { get => rectTransformCache; }
        #endregion
    }
}