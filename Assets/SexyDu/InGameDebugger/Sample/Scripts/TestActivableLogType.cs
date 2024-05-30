using UnityEngine;

namespace SexyDu.InGameDebugger.Sample
{
    public class TestActivableLogType : MonoBehaviour
    {
        private ConsoleHandler ConsoleHandler { get => InGameDebuggerConfig.Ins.Debugger.ConsoleHandler; }

#if true
        private ConsoleFilterLogType filterLogType;
        private ConsoleFilterSearchWord filterSearchWord;
        [SerializeField] private string searchWord;

        private void Start()
        {
            filterLogType = new ConsoleFilterLogType(
                LogType.Log, LogType.Warning,
                LogType.Error, LogType.Assert, LogType.Exception);
            filterSearchWord = new ConsoleFilterSearchWord();

            ConsoleHandler.FilterFollower.FollowFilter(filterLogType);
            ConsoleHandler.FilterFollower.FollowFilter(filterSearchWord);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Add Log"))
            {
                filterLogType.AddLogType(LogType.Log);
            }
            if (GUI.Button(new Rect(100f, 0f, 100f, 100f), "Add Warning"))
            {
                filterLogType.AddLogType(LogType.Warning);
            }
            if (GUI.Button(new Rect(200f, 0f, 100f, 100f), "Add Error"))
            {
                filterLogType.AddLogType(LogType.Error, LogType.Assert, LogType.Exception);
            }

            if (GUI.Button(new Rect(400f, 0f, 100f, 100f), "Remove Log"))
            {
                filterLogType.RemoveLogType(LogType.Log);
            }
            if (GUI.Button(new Rect(500f, 0f, 100f, 100f), "Remove Warning"))
            {
                filterLogType.RemoveLogType(LogType.Warning);
            }
            if (GUI.Button(new Rect(600f, 0f, 100f, 100f), "Remove Error"))
            {
                filterLogType.RemoveLogType(LogType.Error, LogType.Assert, LogType.Exception);
            }

            searchWord = GUI.TextField(new Rect(0f, 100f, 300f, 100f), searchWord);
            if (GUI.Button(new Rect(300f, 100f, 100f, 100f), "Apply"))
            {
                filterSearchWord.SetSearchWord(searchWord);
            }
        }
#else
        private void OnGUI()
        {
            if (GUI.Button(new Rect(0f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.ActivateLogType(LogType.Log);
            }
            if (GUI.Button(new Rect(100f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.ActivateLogType(LogType.Warning);
            }
            if (GUI.Button(new Rect(200f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.ActivateLogType(LogType.Error, LogType.Assert, LogType.Exception);
            }

            if (GUI.Button(new Rect(400f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.InactivateLogType(LogType.Log);
            }
            if (GUI.Button(new Rect(500f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.InactivateLogType(LogType.Warning);
            }
            if (GUI.Button(new Rect(600f, 0f, 100f, 100f), ""))
            {
                ConsoleHandler.InactivateLogType(LogType.Error, LogType.Assert, LogType.Exception);
            }
        }
#endif
    }
}