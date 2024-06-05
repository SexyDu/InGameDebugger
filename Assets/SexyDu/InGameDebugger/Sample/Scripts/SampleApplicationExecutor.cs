#define TESTING_SAMPLE

using UnityEngine;

namespace SexyDu.InGameDebugger.Sample
{
    public class SampleApplicationExecutor : MonoBehaviour
    {
        private InGameDebuggerConfig DebuggerConfig => InGameDebuggerConfig.Ins;

#if TESTING_SAMPLE
        [SerializeField] private Debugger debugger;
#endif
        [SerializeField] private SampleContentsRoot contents; // 샘플 컨텐츠 루트

        private void Awake()
        {
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
                DebuggerConfig.CreateDebugger();
#endif
            }

            contents.Initialize();
        }
    }
}