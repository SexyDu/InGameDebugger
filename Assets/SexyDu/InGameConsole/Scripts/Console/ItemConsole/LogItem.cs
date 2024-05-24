using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SexyDu.InGameConsole
{
    public class LogItem : MonoBehaviour
    {
        // 로그 메세지
        private ILogMessage message = null;
        public ILogMessage Message { get => message; }

        [SerializeField] private Image background; // 배경 이미지
        [SerializeField] private Image icon; // 아이콘 이미지
        [SerializeField] private TMP_Text text; // 텍스트

        private InGameConsoleConfig Config { get => InGameConsoleConfig.Ins; }

        /// <summary>
        /// 로그 메세지 설정
        /// </summary>
        public void Set(ILogMessage message)
        {
            // 활성화 되어있는 경우 맨뒤로
            if (GameObjectCache.activeSelf)
                TransformCache.SetAsLastSibling();
            // 활성화 되어 있지 않은 경우 활성화
            else
                SetActive(true);

            this.message = message;

            icon.sprite = Config.GetLogIcon(this.message.Type);
            icon.color = GetIconColor();

            text.SetText(this.message.GetLogString());
        }

        /// <summary>
        /// 클리어
        /// </summary>
        public void Clear()
        {
            message = null;

            icon.sprite = null;
            icon.color = Color.clear;

            text.SetText(string.Empty);

            SetActive(false);
        }
        /// <summary>
        /// 아이콘 설정 상태에 따른 아이콘 색상 반환
        /// * 이미지가 비어있는 경우 안보이게 하기 위함
        private Color GetIconColor()
        {
            return icon.sprite == null ? Color.clear : Color.white;
        }

        /// <summary>
        /// 배경색상 설정
        /// </summary>
        public void SetBackgroundColor(Color color)
        {
            background.color = color;
        }

        /// <summary>
        /// 로그 아이템 클릭 이벤트 함수
        /// </summary>
        public void OnClick()
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
        private LogDetail CreateLogDetail()
        {
            LogDetail source = Resources.Load<LogDetail>(LogDetail.ResourcesPath);

            return Instantiate(source, Config.Debugger.TransformCache);
        }
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private Transform transformCache;
        private Transform TransformCache { get => transformCache; }
        public Transform Parent { get => TransformCache.parent; }

        [SerializeField] private GameObject gameObjectCache;
        private GameObject GameObjectCache { get => gameObjectCache; }

        private void SetActive(bool active)
        {
            GameObjectCache.SetActive(active);
        }
        #endregion
    }
}