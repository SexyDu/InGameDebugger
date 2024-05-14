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
        [SerializeField] private TMP_FontAsset fontAsset;
        public TMP_FontAsset FontAsset { get { return fontAsset; } }
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