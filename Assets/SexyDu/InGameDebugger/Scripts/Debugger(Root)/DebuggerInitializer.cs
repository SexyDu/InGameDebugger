using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger
{
    public class DebuggerInitializer : MonoBehaviour, IReleasable
    {
        private InGameDebuggerSettings Settings => InGameDebuggerConfig.Ins.Settings;

        public DebuggerInitializer Initialize()
        {
            InitializeCanvas();

            InitializeTextMeshes();

            return this;
        }

        public void Release()
        {
            Destroy(this);
        }

        #region Canvas
        [Header("Canvas")]
        [SerializeField] private Canvas canvas;

        private void InitializeCanvas()
        {
            if (!string.IsNullOrEmpty(Settings.SortingLayerName))
                canvas.sortingLayerName = Settings.SortingLayerName;

            canvas.sortingOrder = Settings.SortingOrder;

            // CanvasBaseWidth 값이 있는 경우
            if (Settings.CanvasBaseWidth > 0)
            {
                // Orientaion을 고려한 Portrait 기준의 화면 width 값
                float width = canvas.renderingDisplaySize.y < canvas.renderingDisplaySize.x ?
                    canvas.renderingDisplaySize.y : canvas.renderingDisplaySize.x;
                // Portrait 기준 width값이 기준보다 작은 경우 scaleFactor 조절
                if (width < Settings.CanvasBaseWidth)
                    canvas.scaleFactor = width / Settings.CanvasBaseWidth;
            }
            else
                Debug.LogWarning("InGameDebuggerSettings에 CanvasBaseWidth 값이 없어 scaleFactor를 설정하지 않았습니다.");
        }
        #endregion

        #region Font
        [Header("Font")]
        [SerializeField] private TMP_Text[] textMeshes;
        [SerializeField] private TMP_InputField[] inputFields;
        private void InitializeTextMeshes()
        {
            for (int i = 0; i < textMeshes.Length; i++)
            {
                textMeshes[i].font = Settings.FontAsset;
            }

            for (int i = 0; i < inputFields.Length; i++)
            {
                inputFields[i].fontAsset = Settings.FontAsset;
            }
        }
        #endregion

#if UNITY_EDITOR
        /// <summary>
        /// TMPro 관련 컴포넌트를 모두 찾아 연결
        /// </summary>
        public void SetTMProComponents()
        {
            textMeshes = GetComponentsInChildren<TMP_Text>(true);
            inputFields = GetComponentsInChildren<TMP_InputField>(true);
        }
#endif
    }
}