using UnityEngine;

namespace SexyDu.InGameDebugger.Sample
{
    public class SampleContentsRoot : MonoBehaviour
    {
        [SerializeField] public TestCommand command;

        public void Initialize()
        {
            command.Bind();
        }

        public void Clear()
        {
            command.Unbind();
        }
    }
}