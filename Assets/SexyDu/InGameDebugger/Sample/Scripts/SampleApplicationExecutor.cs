using UnityEngine;
using SexyDu.InGameDebugger;

namespace SexyDu.InGameDebugger.Sample
{
    public class SampleApplicationExecutor : MonoBehaviour
    {
        private InGameDebuggerConfig DebuggerConfig => InGameDebuggerConfig.Ins;
        [SerializeField] private Debugger debugger; // [임시] 추후 이건 Resources.Load로 가져와서 설정하자!

        [SerializeField] private SampleContentsRoot contents; // 샘플 컨텐츠 루트

        private void Awake()
        {
            if (DebuggerConfig.Settings.ActiveDebugger)
            {
#if true
                debugger = DebuggerConfig.CreateDebugger();
#else
                if (debugger == null)
                {
                    debugger = DebuggerConfig.CreateDebugger();
                }
                else
                {
                    debugger.gameObject.SetActive(true);
                    DebuggerConfig.Debugger = debugger;
                }
#endif
                debugger.Initialize();
            }
            
            contents.Initialize();
        }
    }
}