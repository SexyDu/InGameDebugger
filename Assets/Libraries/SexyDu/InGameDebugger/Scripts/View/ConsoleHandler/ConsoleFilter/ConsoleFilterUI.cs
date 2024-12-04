using System;
using UnityEngine;
using TMPro;

namespace SexyDu.InGameDebugger.View
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

        /// <summary>
        /// 수집된 로그 카운트 표시 클래스
        /// </summary>
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
                SetLogCount(textCountGeneralError, subject.GeneralErrorCount);
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
                        SetLogCount(textCountGeneralError, subject.GeneralErrorCount);
                        break;
                }
            }

            private const int MaxCount = 999; // 표시 최대 수
            private const string MaxString = "999+"; // 표시 최대 수 초과 시 문자열
            [SerializeField] private TMP_Text textCountLog; // 로그 카운트 텍스트
            [SerializeField] private TMP_Text textCountWarning; // 경고 카운트 텍스트
            [SerializeField] private TMP_Text textCountGeneralError; // 일반에러 카운트 텍스트

            private void SetLogCount(TMP_Text textCount, int count)
            {
                if (count > MaxCount)
                    textCount.SetText(MaxString);
                else
                    textCount.SetText(count.ToString());
            }
        }
    }
}