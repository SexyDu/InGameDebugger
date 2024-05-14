using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SexyDu.InGameConsole
{
    public class TestSample : MonoBehaviour
    {
        [SerializeField] private TMP_Text logText;
        private List<ILogMessage> logs;
        private int index = 0;

        private void Awake()
        {
            logs = new List<ILogMessage>();
            
            Application.logMessageReceived += OnLogMessageReceived;
        }

        private void OnLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            ILogMessage logMessage = new LogMessage(condition, stackTrace, type, index++);
            logs.Add(logMessage);
            AddLogText(logMessage);
        }

        private void AddLogText(ILogMessage logMessage)
        {
            logText.text = string.Format("{0}\n\n{1}", logText.text, logMessage.GetLogString());
        }

        [SerializeField] private string sampleLog;
        private void OnGUI()
        {
            if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Log"))
            {
                Debug.Log(sampleLog);
            }

            if (GUI.Button(new Rect(100f, 0f, 100f, 100f), "LogAssertion"))
            {
                Debug.LogAssertion(sampleLog);
            }

            if (GUI.Button(new Rect(200f, 0f, 100f, 100f), "LogWarning"))
            {
                Debug.LogWarning(sampleLog);
            }

            if (GUI.Button(new Rect(300f, 0f, 100f, 100f), "LogError"))
            {
                Debug.LogError(sampleLog);
            }

            if (GUI.Button(new Rect(400f, 0f, 100f, 100f), "LogException"))
            {
                throw new System.Exception("Exception");
            }
        }
    }
}