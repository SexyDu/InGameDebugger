using UnityEngine;
using UnityEditor;

namespace SexyDu.InGameDebugger.View
{
    [CustomEditor(typeof(BugCatcher), true)]
    [CanEditMultipleObjects]
    public class BugCatcherEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("SetWordCompletionItems"))
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    BugCatcher component = targets[i] as BugCatcher;
                    component.SetDefaultAnimationSpecs();
                }
            }
        }
    }
}