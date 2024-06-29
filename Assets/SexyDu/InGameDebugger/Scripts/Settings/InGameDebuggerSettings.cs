using System;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// InGameDebugger 설정
    /// ***** Header *****
    /// </summary>
    [CreateAssetMenu(fileName = "InGameDebuggerSettings", menuName = "SexyDu/Settings/InGameDebugger")]
    public partial class InGameDebuggerSettings : ScriptableObject
    {
        public const string ResourcePath = "Installer/InGameDebuggerSettings"; // 세팅 리소스 경로
        private const string SavedDateTimeFormat = "yyyy-MM-dd HH:mm:ss"; // 저장되는 로컬 시간 포맷

        [Tooltip("InGameDebugger 활성화 상태")]
        [SerializeField] private bool activeDebugger;
        // 디버거 활성화 로컬 키
        private const string ActiveDebuggerKey = "InGameDebugger.ActiveDebugger";
        // 디버거 활성화 정보가 로컬에 설정되어 있는지에 대한 여부
        private bool HasLocalActiveDebugger => PlayerPrefs.HasKey(ActiveDebuggerKey);
        // 로컬에 설정된 디버거 활성화 여부
        private bool LocalActiveDebugger => !PlayerPrefs.GetString(ActiveDebuggerKey).Equals(string.Empty);
        // 디버거 활성화 여부
        public bool ActiveDebugger
        {
            get
            {
                // 디버거 활성화 로컬 설정이 있는 경우
                if (HasLocalActiveDebugger)
                    return LocalActiveDebugger;
                else
                    return activeDebugger;
            }
        }

        #region Canvas
        [Header("Canvas")]
        [Tooltip("InGameDebugger의 Sorting layer")]
        [SerializeField] private string sortingLayer;
        public string SortingLayerName => sortingLayer;

        [Tooltip("InGameDebugger의 Order in layer")]
        [SerializeField] private int orderInLayer;
        public int SortingOrder => orderInLayer;

        [Tooltip("InGameDebugger Canvas의 작업기준 width값")]
        [SerializeField] private float canvasBaseWidth;
        public float CanvasBaseWidth => canvasBaseWidth;
        #endregion

        #region TMPro
        [Header("TMPro")]
        [Tooltip("InGameDebugger에 사용될 폰트에셋")]
        [SerializeField] private TMP_FontAsset fontAsset;
        public TMP_FontAsset FontAsset => fontAsset;
        #endregion

        #region Icon
        [Header("Icon")]
        [Tooltip("일반 로그에 사용될 아이콘 Sprite")]
        [SerializeField] private Sprite logIcon;
        [Tooltip("경고 로그(Warning)에 사용될 아이콘 Sprite")]
        [SerializeField] private Sprite warningIcon;
        [Tooltip("에러 로그(Error)에 사용될 아이콘 Sprite")]
        [SerializeField] private Sprite errorIcon;
        #endregion

        #region CLI
        [Header("CommandLineInterface")]
        [Tooltip("(빌드된) 커맨드 활성화 여부")]
        [SerializeField] private bool useCLI = true;
        // 커맨드 활성화 로컬 키
        private const string UseCLIKey = "InGameDebugger.UseCLI";
        // 커맨드 활성화 정보가 로컬에 설정되어 있는지에 대한 여부
        private bool HasLocalUseCLI => PlayerPrefs.HasKey(UseCLIKey);
        // 로컬에 설정된 디버거 활성화 여부
        private bool LocalUseCLI => !PlayerPrefs.GetString(UseCLIKey).Equals(string.Empty);
        // 커맨드 활성화 여부
        public bool UseCLI
        {
            get
            {
                // 디버거 활성화 로컬 설정이 있는 경우
                if (HasLocalUseCLI)
                    return LocalUseCLI;
                else
                    return useCLI;
            }
        }
        #endregion

        #region DebuggerType
        [Header("Debugger")]
        [SerializeField] private DebuggerType debuggerType;
        #endregion

        #region Item Console
        [Header("Item Console")]
        [Tooltip("Item 타입 Debugger(Prefab) 리소스 경로")]
        [SerializeField] private string itemDebuggerPath;

        [Tooltip("Item의 총 수\n* 해당 수를 넘어가는 순간 기존 아이템을 재사용합니다.")]
        [SerializeField] private int itemCount;
        public int ItemCount => itemCount;

        [Tooltip("선택된 아이템에 표시될 색상입니다.")]
        [SerializeField] private Color selectedItemColor;
        public Color SelectedItemColor => selectedItemColor;
        #endregion

        #region Text Console
        [Header("Text Console")]
        [Tooltip("Text 타입 Debugger(Prefab) 리소스 경로")]
        [SerializeField] private string textDebuggerPath;

        [Tooltip("TextMesh가 표시할 최대 길이")]
        [SerializeField] private int maxTextLength;
        public int MaxTextLength => maxTextLength;
        #endregion
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(InGameDebuggerSettings))]
    public class InGameDebuggerSettingsEditor : Editor
    {
        private void OnEnable()
        {
            Debug.LogFormat("OnEnable InGameDebuggerSettings Editor");
        }

        private void OnDisable()
        {
            Debug.LogFormat("OnDisable InGameDebuggerSettings Editor");
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