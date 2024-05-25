using System;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// InGameDebugger의 기본 설정
    /// </summary>
    public class InGameDebuggerConfig
    {
        private static Lazy<InGameDebuggerConfig> ins = new Lazy<InGameDebuggerConfig>(() => new InGameDebuggerConfig());
        public static InGameDebuggerConfig Ins { get => ins.Value; }

        public InGameDebuggerConfig()
        {
            settings = Resources.Load<InGameDebuggerSettings>(InGameDebuggerSettings.ResourcePath).Initialize();
        }

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

        /// <summary>
        /// 로그 타입에 따른 로그 아이콘 Sprite 반환
        /// </summary>
        public Sprite GetLogIcon(LogType type)
        {
            switch (type)
            {
                case LogType.Assert:
                case LogType.Error:
                case LogType.Exception:
                    return Settings.ErrorIcon;
                case LogType.Warning:
                    return Settings.WarningIcon;
                default: // LogType.Log
                    return Settings.LogIcon;
            }
        }
    }
}