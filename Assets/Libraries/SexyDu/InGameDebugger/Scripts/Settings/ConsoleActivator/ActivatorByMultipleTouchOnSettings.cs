#if UNITY_EDITOR || !(UNITY_ANDROID || UNITY_IOS)
#define CONSIDER_DESKTOP
#endif

using System;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    [Serializable]
    public struct ActivatorByMultipleTouchOnSettings
    {
        [Tooltip("콘솔 활성화에 사용될 화면터치 수\n* fingerCount 수의 손가락을 화면에 올리고 있으면 pressureTime초 뒤에 콘솔 활성화")]
        [SerializeField] private int fingerCount;
        [Tooltip("활성화 입력 대기 시간")]
        [SerializeField] private float pressureTime;
#if CONSIDER_DESKTOP
        [Tooltip("콘솔 활성화에 사용될 키 코드\n* keyCode 키를 누르고 있으면 pressureTime초 뒤에 콘솔 활성화")]
        [SerializeField] private KeyCode keyCode; // Default '`' (KeyCode.BackQuote)
#endif

        public ActivatorByMultipleTouch GetActivator()
        {
            return new ActivatorByMultipleTouch(fingerCount, pressureTime)
#if CONSIDER_DESKTOP
                .SetKeyCode(keyCode)
#endif
                ;
        }
    }
}