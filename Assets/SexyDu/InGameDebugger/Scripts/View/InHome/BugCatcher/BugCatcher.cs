//#define USE_ERROR_COUNT

using UnityEngine;
#if USE_ERROR_COUNT
using TMPro;
#endif

namespace SexyDu.InGameDebugger.View
{
    public partial class BugCatcher : MonoInDebuggerHome, IConsoleLogObserver
    {
        Debugger Debugger => InGameDebuggerConfig.Ins.Debugger;

        /// <summary>
        /// 초기 설정
        /// </summary>
        public override MonoInDebuggerHome Initialize()
        {
            InitializeAnimation();

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
#if USE_ERROR_COUNT
            SetGeneralErrorCount(subject.GeneralErrorCount);
#endif
        }

        /// <summary>
        /// 로그 카운트 추가 감지
        /// : IConsoleLogObserver
        /// </summary>
        public void OnDetectLog(IConsoleLogSubject subject, LogType type)
        {
            Animate(type);

#if USE_ERROR_COUNT
            SetGeneralErrorCount(subject.GeneralErrorCount);
#endif
        }

        /// <summary>
        /// 버그 클릭 이벤트
        /// TODO: 추후 현재와 같은 버튼 클릭 방식이 아닌 다른 방식으로 디버거를 활성화 시키도록 수정
        ///  - 예1: 화면을 3터치로 0.5초간 누르고 있는다
        /// </summary>
        public void OnClickBug()
        {
            Debugger.ActivateConsole();
        }

#if USE_ERROR_COUNT
        #region Error Count
        [Header("ErrorCount")]
        [SerializeField] private TMP_Text textMeshGeneralError; // 에러 수 표시 텍스트
        private const int MaximumErrorCount = 999;
        private const string OverErrorCountString = "+999";

        /// <summary>
        /// 에러 수 설정
        /// </summary>
        private void SetGeneralErrorCount(int generalErrorCount)
        {
            if (generalErrorCount > MaximumErrorCount)
                textMeshGeneralError.SetText(OverErrorCountString);
            else
                textMeshGeneralError.SetText(generalErrorCount.ToString());
        }
        #endregion
#endif
    }
}