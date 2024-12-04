using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger.Sample
{
    /// <summary>
    /// [샘플] 예제 컨텐츠 루트
    /// </summary>
    public class SampleContentsRoot : MonoBehaviour, IFramerateObserver
    {
        [SerializeField] public TestLogging logging;
        [SerializeField] public TestCommand command;

        public void Initialize()
        {
            command.Bind();

            logging.LogPromised();

            framerate = new FramerateSubject(0.5f);
            framerate.Subscribe(this);
        }

        public void Clear()
        {
            command.Unbind();

            framerate.Unsubscribe(this);
            framerate = null;
        }

        private void OnEnable()
        {
            if (InGameDebuggerConfig.Ins.Debugger != null)
            {
                InGameDebuggerConfig.Ins.Debugger.FramerateSubject.Subscribe(this);
            }
        }

        private void OnDisable()
        {
            if (InGameDebuggerConfig.Ins.Debugger != null)
            {
                InGameDebuggerConfig.Ins.Debugger.FramerateSubject.Unsubscribe(this);
            }
        }

        #region Framerate
        private FramerateSubject framerate;
        [SerializeField] private TMP_Text textMeshFramerate;
        public void OnReceivedFrameRate(float framerate)
        {
            textMeshFramerate.SetText(framerate.ToString("F2"));
        }
        #endregion
    }
}