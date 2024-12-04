#define USE_READONLY
#if UNITY_EDITOR
/// 아래 전처리기를 활성화 하면 하이어라키에 표시되는 LogItem에 ForEditor가 붙어 값이 제대로 설정되었는지 알 수 있다.
//#define USE_FOREDITOR
#endif
// 변형 Queue(class QueueLogItem) 형식의 아이템 관리 구조 사용
#define USE_QUEUE

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SexyDu.UI.UGUI;
#if USE_FOREDITOR
using SexyDu.InGameDebugger.ForEditor;
#endif

namespace SexyDu.InGameDebugger
{
    [Serializable]
    public class LogItem : ILogItem
    {
#if USE_READONLY
        private readonly Image background; // 배경 이미지
        private readonly NullableImage icon; // 아이콘 이미지
        private readonly TMP_Text text; // 텍스트
        private readonly RectTransform rectTransformCache; // RectTransform
        private readonly GameObject gameObjectCache; // GameObject

        public LogItem(Image background, Image icon, TMP_Text text, RectTransform rectTransformCache, GameObject gameObjectCache)
        {
            this.background = background; // 배경 이미지
            this.icon = new NullableImage(icon); // 아이콘 이미지
            this.text = text; // 텍스트
            this.rectTransformCache = rectTransformCache; // RectTransform
            this.gameObjectCache = gameObjectCache; // GameObject

#if USE_FOREDITOR
            LogItemForEditor forEditor
                = this.gameObjectCache.AddComponent<LogItemForEditor>()
                .SetBackground(background)
                .SetIcon(icon)
                .SetTextMesh(text)
                .SetRectTransform(rectTransformCache)
                .SetGameObject(gameObjectCache);
#endif
        }
#else // Builder Pattern
        private Image background; // 배경 이미지
        private Image icon; // 아이콘 이미지
        private TMP_Text text; // 텍스트
        private RectTransform rectTransformCache; // RectTransform
        private GameObject gameObjectCache; // GameObject

        public LogItem SetBackground(Image image)
        {
            this.background = image;

            return this;
        }
        public LogItem Set(Image image)
        {
            this.icon = image;

            return this;
        }
        public LogItem Set(TMP_Text text)
        {
            this.text = text;

            return this;
        }
        public LogItem Set(RectTransform rectTransformCache)
        {
            this.rectTransformCache = rectTransformCache;

            return this;
        }
        public LogItem Set(GameObject gameObjectCache)
        {
            this.gameObjectCache = gameObjectCache;

#if USE_FOREDITOR
            LogItemForEditor forEditor
                = this.gameObjectCache.AddComponent<LogItemForEditor>()
                .SetBackground(background)
                .SetIcon(icon)
                .SetTextMesh(text)
                .SetRectTransform(rectTransformCache)
                .SetGameObject(gameObjectCache);
#endif

            return this;
        }
#endif

        /// <summary>
        /// 아이템 클릭에 대한 버튼 이벤트 연결
        /// </summary>
        public LogItem AddButtonListener(Button.ButtonClickedEvent onClick)
        {
            // 전달달받은 이벤트에 OnClick 함수 연결
            onClick.AddListener(OnClick);

            return this;
        }

        // 로그 메세지
        private ILogMessage message = null;
        // : ILogItem
        public ILogMessage Message { get => message; }

        private InGameDebuggerConfig Config { get => InGameDebuggerConfig.Ins; }

        /// <summary>
        /// 로그 메세지 설정
        /// : ILogItem
        /// </summary>
        public void Set(ILogMessage message)
        {
#if !USE_QUEUE
            // 설정과 동시에 하이어라키 맨 아래로 이동
            /// * 설정과 동시에 디스플레이의 최하단에 보이도록 하기 위함
            rectTransformCache.SetAsLastSibling();
#endif
            // 활성화 되어 있지 않은 경우 활성화
            if (!gameObjectCache.activeSelf)
                SetActive(true);

            this.message = message;

            icon.SetSprite(Config.Settings.GetLogIcon(this.message.Type));

            text.SetText(this.message.GetLogString());
        }

        /// <summary>
        /// 클리어
        /// : ILogItem (IClearable)
        /// </summary>
        public void Clear()
        {
            message = null;

            icon.SetSprite(null);

            text.SetText(string.Empty);

            SetActive(false);
        }

        /// <summary>
        /// 배경 색상 설정
        /// : ILogItem
        /// </summary>
        public ILogItem SetBackgroundColor(Color color)
        {
            background.color = color;

            return this;
        }

        /// <summary>
        /// 로그 아이템 클릭 이벤트 함수
        /// </summary>
        private void OnClick()
        {
            Color existing = background.color;
            SetBackgroundColor(Config.Settings.SelectedItemColor);

            CreateLogDetail()
                .Initialize()
                .Set(message)
                .CallbackOnClose(() => {
                    SetBackgroundColor(existing);
                });
        }

        #region LogDetail
        /// <summary>
        /// 로그 상세보기 팝업 생성
        /// </summary>
        /// <returns></returns>
        private LogDetail CreateLogDetail()
        {
            LogDetail source = Resources.Load<LogDetail>(LogDetail.ResourcesPath);

            return MonoBehaviour.Instantiate(source, Config.Debugger.TransformCache);
        }
        #endregion

        #region ObjectCache
        /// <summary>
        /// 활성화 상태
        /// : ILogItem
        /// </summary>
        public bool Activated => gameObjectCache.activeSelf;

        /// <summary>
        /// 활성화 상태 설정
        /// </summary>
        private void SetActive(bool active)
        {
            gameObjectCache.SetActive(active);
        }

        /// <summary>
        /// 위치값
        /// : ILogItem
        /// </summary>
        public Vector2 anchoredPosition
        {
            get => rectTransformCache.anchoredPosition;
            set { rectTransformCache.anchoredPosition = value; }
        }
        #endregion
    }
}