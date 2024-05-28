using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger.Sample
{
    public class TestActivableLogType : MonoBehaviour
    {
        private ConsoleHandler ConsoleHandler { get => InGameDebuggerConfig.Ins.Debugger.ConsoleHandler; }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.ActivateLogType(LogType.Log);
            }
            if (GUI.Button(new Rect(100f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.ActivateLogType(LogType.Warning);
            }
            if (GUI.Button(new Rect(200f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.ActivateLogType(LogType.Error, LogType.Assert, LogType.Exception);
            }

            if (GUI.Button(new Rect(400f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.InactivateLogType(LogType.Log);
            }
            if (GUI.Button(new Rect(500f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.InactivateLogType(LogType.Warning);
            }
            if (GUI.Button(new Rect(600f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.InactivateLogType(LogType.Error, LogType.Assert, LogType.Exception);
            }
        }
    }
}