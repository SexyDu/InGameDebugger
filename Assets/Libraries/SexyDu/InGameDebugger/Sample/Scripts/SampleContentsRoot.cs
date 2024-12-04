using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger.Sample
{
    /// <summary>
    /// [샘플] 예제 컨텐츠 루트
    /// </summary>
    public class SampleContentsRoot : MonoBehaviour
    {
        [SerializeField] public TestLogging logging;
        [SerializeField] public TestCommand command;

        public void Initialize()
        {
            command.Bind();

            logging.LogPromised();
        }

        public void Clear()
        {
            command.Unbind();
        }
    }
}