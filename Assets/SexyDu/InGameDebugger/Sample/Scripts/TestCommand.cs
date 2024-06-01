using UnityEngine;

namespace SexyDu.InGameDebugger.Sample
{
    public class TestCommand : MonoBehaviour
    {
        private ICommandContainer CommandDictionary => InGameDebuggerConfig.Ins.CommandDictionary;

        private void Awake()
        {
            CommandDictionary.Bind("abcd", (parameters) => {
                Debug.LogFormat("Execute abcd");
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Debug.LogFormat("- {0}", parameters[i]);
                    }
                }
            });

            CommandDictionary.Bind("abcdef", (parameters) => {
                Debug.LogFormat("Execute abcdef");
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Debug.LogFormat("- {0}", parameters[i]);
                    }
                }
            });

            CommandDictionary.Bind("abcdefgggg", (parameters) => {
                Debug.LogFormat("Execute abcdefgggg");
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Debug.LogFormat("- {0}", parameters[i]);
                    }
                }
            });

            CommandDictionary.Bind("kkzz", (parameters) => {
                Debug.LogFormat("Execute kkzz");
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Debug.LogFormat("- {0}", parameters[i]);
                    }
                }
            });
        }
    }
}