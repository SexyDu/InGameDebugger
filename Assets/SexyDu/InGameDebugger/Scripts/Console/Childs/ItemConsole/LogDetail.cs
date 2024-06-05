using System;
using UnityEngine;
using TMPro;
using SexyDu.UGUI;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 로그 상세 보기
    /// </summary>
    public class LogDetail : MonoBehaviour, IClearable, IDestroyable
    {
        public const string ResourcesPath = "IGDPrefabs/LogDetail";

        // 로그 메세지
        private ILogMessage message = null;
        // 아이콘 이미지
        [SerializeField] private NullableImage icon;
        // 로그 텍스트메쉬
        [SerializeField] private TMP_Text text;
        // StackTrace 크기 비율 (텍스트메쉬 폰트 크기 기준)
        [SerializeField] private float stackTraceRatio;
        // StackTrace가 표시될 영역의 텍스트 포맷
        /// {0} : 폰트 크기
        /// {1} : StackTrace 문자열
        private const string StackTraceFormat = "<size={0}>{1}</size>";

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
            // 메세지 설정
            this.message = message;

            /// 아이콘 설정
            icon.sprite = InGameDebuggerConfig.Ins.Settings.GetLogIcon(this.message.Type);

            /// 텍스트 설정
            text.SetText(string.Format("{0}\n{1}", this.message.Condition, GetStackTraceString(this.message)));

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
        /// 텍스트 메쉬에 표시될 StackTrace 문자열 반환
        /// </summary>
        private string GetStackTraceString(ILogMessage message)
        {
            float stackTraceFontSize = text.fontSize * stackTraceRatio;

            return string.Format(StackTraceFormat, stackTraceFontSize, message.StackTrace);
        }

        /// <summary>
        /// 클리어
        /// </summary>
        public void Clear()
        {
            icon.sprite = null;
            text.SetText(string.Empty);
        }

        /// <summary>
        /// 파괴
        /// </summary>
        public void Destroy()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 로그 메세지 복사
        /// </summary>
        private void CopyLog()
        {
            GUIUtility.systemCopyBuffer = string.Format("{0}\n{1}", message.Condition, message.StackTrace);
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
            CopyLog();
        }

        #region ObjectCaches
        [Header("ObjectCaches")]
        [SerializeField] private RectTransform rectTransformCache;
        private RectTransform RectTransformCache { get => rectTransformCache; }
        #endregion
    }
}