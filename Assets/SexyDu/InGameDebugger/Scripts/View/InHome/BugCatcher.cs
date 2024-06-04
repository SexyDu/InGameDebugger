using UnityEngine;

namespace SexyDu.InGameDebugger.View
{
    public class BugCatcher : MonoInDebuggerHome, IConsoleLogObserver
    {
        Debugger Debugger => InGameDebuggerConfig.Ins.Debugger;

        /// <summary>
        /// 초기 설정
        /// </summary>
        /// <returns></returns>
        public override MonoInDebuggerHome Initialize()
        {
            Debugger.ConsoleLogSubject.Subscribe(this);

            return this;
        }

        /// <summary>
        /// 클리어
        /// </summary>
        public override void Clear()
        {
            Debugger.ConsoleLogSubject.Unsubscribe(this);
        }

        /// <summary>
        /// 로그 카운트 변경 감지
        /// : IConsoleLogObserver
        /// </summary>
        public void OnDetectLog(IConsoleLogSubject subject)
        {
            .
        }

        /// <summary>
        /// 로그 카운트 추가 감지
        /// : IConsoleLogObserver
        /// </summary>
        public void OnDetectLog(IConsoleLogSubject subject, LogType type)
        {

        }

        /// <summary>
        /// 버그 클릭 이벤트
        /// </summary>
        public void OnClickBug()
        {
            Debugger.ActivateConsole();
        }
    }
}