using System;
using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger.Sample
{
    /// <summary>
    /// [샘플[ 테스트 로그 처리
    /// </summary>
    public class TestLogging : MonoBehaviour
    {
        [SerializeField] private TMP_InputField logInputField;

        public void LogPromised()
        {
            Debug.Log("일반 로그입니다.");
            Debug.LogWarning("Warning 로그입니다.");
            Debug.LogError("Error 로그입니다.");
            Debug.LogAssertion("Assert 로그입니다.");
            Debug.LogException(new Exception("Exception 로그입니다."));
        }

        public void OnClickLog()
        {
            Debug.Log(logInputField.text);
        }

        public void OnClickWarning()
        {
            Debug.LogWarning(logInputField.text);
        }

        public void OnClickError()
        {
            Debug.LogError(logInputField.text);
        }

        public void OnClickAssertion()
        {
            Debug.LogAssertion(logInputField.text);
        }

        public void OnClickException()
        {
            throw new Exception(logInputField.text);
        }

        public void OnClickToggleDebugger()
        {
            InGameDebuggerConfig.Ins.Settings.SetActive(!InGameDebuggerConfig.Ins.Settings.ActiveDebugger);

            if (InGameDebuggerConfig.Ins.Settings.ActiveDebugger)
            {
                InGameDebuggerConfig.Ins.CreateDebugger();
            }
            else
            {
                InGameDebuggerConfig.Ins.DestroyDebugger();
            }
        }
    }
}