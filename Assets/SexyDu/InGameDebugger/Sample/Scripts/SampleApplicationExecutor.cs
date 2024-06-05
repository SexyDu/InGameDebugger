//#define TESTING_SAMPLE

using UnityEngine;

namespace SexyDu.InGameDebugger.Sample
{
    /// <summary>
    /// [샘플] 앱 실행 관리
    /// </summary>
    public class SampleApplicationExecutor : MonoBehaviour
    {
        private InGameDebuggerConfig DebuggerConfig => InGameDebuggerConfig.Ins;

#if TESTING_SAMPLE
        [SerializeField] private Debugger debugger;
#endif
        [SerializeField] private SampleContentsRoot contents; // 샘플 컨텐츠 루트

        private void Awake()
        {
            // Debugger 활성화 상태인 경우
            if (DebuggerConfig.Settings.ActiveDebugger)
            {
#if TESTING_SAMPLE
                if (debugger == null)
                    DebuggerConfig.CreateDebugger();
                else
                {
                    DebuggerConfig.Debugger = debugger;
                    debugger.Initialize();
                }
#else
                // Debugger 생성
                DebuggerConfig.CreateDebugger();
#endif
            }

            contents.Initialize();
        }
    }
}