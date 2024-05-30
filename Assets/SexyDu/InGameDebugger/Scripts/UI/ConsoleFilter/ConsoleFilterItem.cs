using UnityEngine;

namespace SexyDu.InGameDebugger.UI
{
    public abstract class ConsoleFilterItem : MonoBehaviour
    {
        public abstract IConsoleFilter Filter { get; }
    }
}