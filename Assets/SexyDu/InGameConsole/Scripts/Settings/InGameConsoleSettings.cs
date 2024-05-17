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