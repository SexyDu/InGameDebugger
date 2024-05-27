using System;
using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger.Sample
{
    public class TestLogging : MonoBehaviour
    {
        [SerializeField] private TMP_InputField logInputField;

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
    }
}