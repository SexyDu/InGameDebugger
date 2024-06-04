using UnityEngine;

namespace SexyDu.InGameDebugger.View
{
    public abstract class ConsoleFilterItem : MonoBehaviour
    {
        public abstract IConsoleFilter Filter { get; }
    }
}