using System;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// InGameDebugger 설정
    /// </summary>
    public partial class InGameDebuggerSettings
    {
        public InGameDebuggerSettings Initialize()
        {
            return this;
        }

        #region DebuggerType
        /// <summary>
        /// 현재 선택된 Debugger 리소스 경로
        /// </summary>
        public string DebuggerPath
        {
            get
            {
                switch (debuggerType)
                {
                    case DebuggerType.Item:
                        return itemDebuggerPath;
                    case DebuggerType.Text:
                        return textDebuggerPath;
                    default:
                        throw new NotImplementedException($"아직 정의되지 않은 DebuggerType({debuggerType})입니다");
                }
            }
        }
        #endregion

        #region Command
        /// <summary>
        /// 커맨트 로컬 활성화 상태 설정
        /// </summary>
        public void SetCommandEnable(bool enable)
        {
            if (enable)
            {
                // 혹시 사용할 일이 있을지 모르지만... 값은 활성화 시간으로...
                DateTime now = DateTime.Now;
                PlayerPrefs.SetString(UseCLIKey, now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                PlayerPrefs.DeleteKey(UseCLIKey);
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
                case LogType.Log:
                    return logIcon;
                case LogType.Warning:
                    return warningIcon;
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    return errorIcon;
                default:
                    return null;
            }
        }
    }
}