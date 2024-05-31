using System;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// InGameDebugger의 기본 설정
    /// </summary>
    public class InGameDebuggerConfig
    {
        #region Singleton
        private static Lazy<InGameDebuggerConfig> ins = new Lazy<InGameDebuggerConfig>(() => new InGameDebuggerConfig());
        public static InGameDebuggerConfig Ins { get => ins.Value; }

        public InGameDebuggerConfig()
        {
            // Settings 로드
            settings = Resources.Load<InGameDebuggerSettings>(InGameDebuggerSettings.ResourcePath)?.Initialize();
            if (settings == null)
                throw new NullReferenceException($"지정된 경로({InGameDebuggerSettings.ResourcePath})에 맞는 InGameDebuggerSettings를 찾을 수 없습니다.");

            
        }
        #endregion

        /// Settings
        private readonly InGameDebuggerSettings settings = null;
        public InGameDebuggerSettings Settings { get => settings; }

        #region Debugger
        /// <summary>
        /// 메인 디버거
        /// </summary>
        private Debugger debugger = null;
        public Debugger Debugger
        {
            get => debugger;

            set
            {
                if (debugger != null)
                {
                    Debug.LogErrorFormat($"새로운 Debugger가 확인되어 기존 Debugger({debugger.name})를 제거합니다.");
                    debugger.Destroy();
                    debugger = null;
                }

                debugger = value;
            }
        }
        #endregion
    }
}