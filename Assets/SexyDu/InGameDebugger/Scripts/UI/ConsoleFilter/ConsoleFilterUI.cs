using System;
using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger.UI
{
    /// <summary>
    /// 필터 UI 관리 클래스
    /// </summary>
    public class ConsoleFilterUI : MonoBehaviour
    {
        /// <summary>
        /// 초기 설정
        /// </summary>
        public void Initialize(IConsoleFilterFollower filterFollower, IConsoleLogSubject consoleLogSubject)
        {
            // 초기 설정 및 필터 팔로우
            filterSearchWord.Initialize();
            filterFollower.FollowFilter(filterSearchWord.Filter);

            // 초기 설정 및 필터 팔로우
            filterLogType.Initialize();
            filterFollower.FollowFilter(filterLogType.Filter);

            // 로그 옵저버 등록 후 값 설정
            consoleLogSubject.Subscribe(consoleLogObserver);
            consoleLogObserver.OnDetectLog(consoleLogSubject);
        }

        // 검색어 필터
        [SerializeField] private FilterItemSearchWord filterSearchWord;

        // 로그 타입 필터
        [SerializeField] private FilterItemLogType filterLogType;

        // 로그 옵저버
        [SerializeField] private ConsoleLogObserver consoleLogObserver;

        [Serializable]
        public class ConsoleLogObserver : IConsoleLogObserver
        {
            /// <summary>
            /// 로그 카운트 변경 감지
            /// </summary>
            public void OnDetectLog(IConsoleLogSubject subject)
            {
                SetLogCount(textCountLog, subject.LogCount);
                SetLogCount(textCountWarning, subject.WarningCount);
                SetLogCount(textCountGeneralError, subject.ErrorCount);
            }

            /// <summary>
            /// 로그 카운트 추가 감지
            /// </summary>
            public void OnDetectLog(IConsoleLogSubject subject, LogType type)
            {
                switch (type)
                {
                    case LogType.Log:
                        SetLogCount(textCountLog, subject.LogCount);
                        break;
                    case LogType.Warning:
                        SetLogCount(textCountWarning, subject.WarningCount);
                        break;
                    default: // case LogType.Error: case LogType.Assert: case LogType.Exception:
                        SetLogCount(textCountGeneralError, subject.ErrorCount);
                        break;
                }
            }

            private const int MaxCount = 999;
            private const string MaxString = "999+";
            [SerializeField] private TMP_Text textCountLog;
            [SerializeField] private TMP_Text textCountWarning;
            [SerializeField] private TMP_Text textCountGeneralError;

            private void SetLogCount(TMP_Text textCount, int count)
            {
                if (count > MaxCount)
                {
                    textCount.SetText(MaxString);
                }
                else
                {
                    textCount.SetText(count.ToString());
                }
            }
        }
    }
}