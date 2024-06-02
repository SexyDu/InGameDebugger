using UnityEngine;
using SexyDu.InGameDebugger;

namespace SexyDu.Sample
{
    public class SampleApplicationExecutor : MonoBehaviour
    {
        private InGameDebuggerConfig DebuggerConfig => InGameDebuggerConfig.Ins;
        [SerializeField] private Debugger debugger; // [임시] 추후 이건 Resources.Load로 가져와서 설정하자!

        private void Awake()
        {
            if (DebuggerConfig.Settings.ActiveDebugger)
            {
                /// [임시] 추후 이건 Resources.Load로 가져와서 설정하자!
                debugger.gameObject.SetActive(true);
                debugger.Initialize();
            }
        }

    }
}