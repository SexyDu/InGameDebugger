using UnityEngine;
using UnityEditor;

namespace SexyDu.InGameDebugger.UI
{
    [CustomEditor(typeof(TerminalAutoComplete))]
    public class TerminalAutoCompleteEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("SetWordCompletionItems"))
            {
                TerminalAutoComplete component = target as TerminalAutoComplete;
                component.SetWordCompletionItems();
            }
        }
    }
}