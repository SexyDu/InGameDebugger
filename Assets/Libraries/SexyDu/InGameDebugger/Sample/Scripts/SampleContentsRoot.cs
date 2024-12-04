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

            framerate = new FramerateSubject();
            framerate.Run(this, 0.5f);
        }

        public void Clear()
        {
            command.Unbind();

            framerate.Stop();
            framerate = null;
        }

        #region Framerate
        private FramerateSubject framerate;
        [SerializeField] private TMP_Text textMeshFramerate;
        public void OnReceivedFrameRate(float framerate)
        {
            textMeshFramerate.SetText(framerate.ToString());
        }
        #endregion
    }
}