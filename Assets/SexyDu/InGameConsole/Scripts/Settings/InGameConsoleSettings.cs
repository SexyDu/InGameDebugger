using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SexyDu.InGameConsole
{
    [CreateAssetMenu(fileName = "InGameConsoleSettings", menuName = "SexyDu/Settings/InGameConsole")]
    public class InGameConsoleSettings : ScriptableObject
    {
        public const string ResourcePath = "Installer/InGameConsoleSettings";

        [Tooltip("InGameConsole에 사용될 폰트에셋")]
        [SerializeField] private TMP_FontAsset fontAsset;
        public TMP_FontAsset FontAsset { get { return fontAsset; } }

        [Tooltip("로그 Scroll 시 로그 출력 Pause 여부\n* 체크되어 있는 경우 스크롤 시 로그 출력이 멈춥니다.")]
        [SerializeField] private bool useScrollPause = true;
        public bool UseScrollPause { get { return useScrollPause; } }

        [Header("Canvas")]
        [Tooltip("InGameConsole의 Sorting layer")]
        [SerializeField] private string sortingLayer;
        public string SortingLayer { get { return sortingLayer; } }
        
        [Tooltip("InGameConsole의 Order in layer")]
        [SerializeField] private int orderInLayer;
        public int OrderInLayer { get { return orderInLayer; } }

        #region Item Console
        [Header("Item Console")]
        [Tooltip("Item의 총 수\n* 해당 수를 넘어가는 순간 기존 아이템을 재사용합니다.")]
        [SerializeField] private int itemCount;
        public int ItemCount { get => itemCount; }

        [Tooltip("선택된 아이템에 표시될 색상입니다.")]
        [SerializeField] private Color selectedItemColor;
        public Color SelectedItemColor { get => selectedItemColor; }

        [Header("Icon")]
        [Tooltip("일반 로그에 사용될 아이콘 Sprite")]
        [SerializeField] private Sprite logIcon;
        [Tooltip("경고 로그(Warning)에 사용될 아이콘 Sprite")]
        [SerializeField] private Sprite warningIcon;
        [Tooltip("에러 로그(Error)에 사용될 아이콘 Sprite")]
        [SerializeField] private Sprite errorIcon;
        public Sprite LogIcon { get => logIcon; }
        public Sprite WarningIcon { get => warningIcon; }
        public Sprite ErrorIcon { get => errorIcon; }
        #endregion

        public InGameConsoleSettings Initialize()
        {
            return this;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(InGameConsoleSettings))]
    public class InGameConsoleSettingsEditor : Editor
    {
        private void OnEnable()
        {
            Debug.LogFormat("OnEnable InGameConsoleSettings Editor");
        }

        private void OnDisable()
        {
            Debug.LogFormat("OnDisable InGameConsoleSettings Editor");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(this);
            }
        }
    }
#endif
}