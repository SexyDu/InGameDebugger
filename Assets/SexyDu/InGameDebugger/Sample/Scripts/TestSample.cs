using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SexyDu.Tool;

namespace SexyDu.InGameDebugger.Sample
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
            ILogMessage logMessage = new LogMessageTest(condition, stackTrace, type, index++);
            logs.Add(logMessage);
            AddLogText(logMessage);
        }

        private void AddLogText(ILogMessage logMessage)
        {
            logText.text = string.Format("{0}\n\n{1}", logText.text, logMessage.GetLogString());
        }

        [SerializeField] private string sampleLog;
        [SerializeField] private Canvas testCanvas;
        private void OnGUI()
        {
            if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Log"))
            {
                Debug.LogFormat("{0}", sampleLog);
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
#if false
            if (GUI.Button(new Rect(0f, 100f, 100f, 100f), ""))
            {
                if (safeArea != null)
                {
                    safeArea.Set(testCanvas);
                }

                if (safeAreaAnchor != null)
                {
                    safeAreaAnchor.Set();
                }
            }
            if (GUI.Button(new Rect(100f, 100f, 100f, 100f), ""))
            {
                Debug.LogFormat("renderingDisplaySize : [{0}, {1}], scaleFactor : {2}",
                    testCanvas.renderingDisplaySize.x, testCanvas.renderingDisplaySize.y,
                    testCanvas.scaleFactor);
                Debug.LogFormat("ScreenSize : [{0}, {1}]", Screen.width, Screen.height);
                Debug.LogFormat("SafeArea Size : [{0}, {1}], Position : [{2}, {3}]",
                    Screen.safeArea.size.x, Screen.safeArea.size.y,
                    Screen.safeArea.position.x, Screen.safeArea.position.y);
                // renderingDisplaySize와 ScreenSize은 동일
                // Screen.safeArea.position는 바닥기준 표지션인것으로 확인됨
                //  - landscape일 경우 확인 고려 필요 : 뭔가 좌측하단 기준 느낌임
                // 결국 적용할 떄 safeArea / Canvas.scaleFactor 계산하면 될것 같음
            }
#endif
        }

        [SerializeField] private SafeArea safeArea;
        [SerializeField] private SafeAreaAnchor safeAreaAnchor;
    }
}