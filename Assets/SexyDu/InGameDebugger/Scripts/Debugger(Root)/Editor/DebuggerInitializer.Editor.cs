using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SexyDu.InGameDebugger
{
    [CustomEditor(typeof(DebuggerInitializer))]
    [CanEditMultipleObjects]
    public class DebuggerInitializerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("SetWordCompletionItems"))
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    DebuggerInitializer component = targets[i] as DebuggerInitializer;
                    component.SetTMProComponents();
                }
            }
        }
    }
}