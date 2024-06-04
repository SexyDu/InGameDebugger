using System;
using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger.View
{
    /// <summary>
    /// 로그 타입 필터 UI 관리 클래스
    /// </summary>
    public class FilterItemLogType : ConsoleFilterItem
    {
        // 로그 타입 필터
        private ConsoleFilterLogType filter = new ConsoleFilterLogType(
            LogType.Log, LogType.Warning, LogType.Error, LogType.Assert, LogType.Exception);
        public override IConsoleFilter Filter { get => filter; }

        /// <summary>
        /// 초기 설정
        /// </summary>
        public FilterItemLogType Initialize()
        {
            for (int i = 0; i < switches.Length; i++)
            {
                switches[i].Set(true);
            }

            filter = new ConsoleFilterLogType(GetActivatedLogTypeList());

            return this;
        }

        /// <summary>
        /// 현재 활성화된 로그 타입 리스트 반환
        /// </summary>
        private List<LogType> GetActivatedLogTypeList()
        {
            List<LogType> list = new List<LogType>();

            for (int i = 0; i < switches.Length; i++)
            {
                if (switches[i].Active)
                    list.AddRange(switches[i].LogTypes);
            }

            return list;
        }

        // 로그 타입 스위치 배열
        [SerializeField] private LogTypeSwitch[] switches;

        /// <summary>
        /// 배열 인덱스에 따른 로그 타입 스위치 토글
        /// </summary>
        public void OnClickToggle(int index)
        {
            if (index >= 0 && index < switches.Length)
            {
                Toggle(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException("switches",
                    $"Toggle 하려는 switch의 index({index})가 배열의 범위를 벗어났습니다. [ 배열 크기 : {switches.Length} ]");
            }
        }

        /// <summary>
        /// 로그 타입 스위치 토글 처리
        /// </summary>
        private void Toggle(int index)
        {
            switches[index].Toggle();

            if (switches[index].Active)
                filter.AddLogType(switches[index].LogTypes);
            else
                filter.RemoveLogType(switches[index].LogTypes);
        }

        /// <summary>
        /// 로그 타입 토글 관리 구조체
        /// </summary>
        [Serializable]
        public struct LogTypeSwitch
        {
            private bool active; // 활성화 상태
            [SerializeField] private LogType[] logTypes; // 관련 로그 타입
            [SerializeField] private GameObject inactiveSign; // 비활성화 표시

            public bool Active { get => active; }
            public LogType[] LogTypes { get => logTypes; }

            /// <summary>
            /// 활성화 상태 토글
            /// </summary>
            public void Toggle()
            {
                Set(!active);
            }

            /// <summary>
            /// 활성화 상태 설정
            /// </summary>
            public void Set(bool active)
            {
                this.active = active;

                inactiveSign.SetActive(!this.active);
            }
        }
    }
}