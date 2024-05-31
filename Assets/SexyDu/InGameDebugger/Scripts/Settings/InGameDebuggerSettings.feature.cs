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
            // 커맨드가 비활성화 되어있지만 로컬 커맨드가 활성화 되어있는 경우 커맨드 활성화
            if (!useCommand && UseCommandInLocal)
                useCommand = true;

            return this;
        }

        #region Command
        // 커맨드 로컬 활성화 키
        private const string UseCommandKey = "InGameDebugger.UseCommand";

        // 커맨드 로컬 화성화 여부
        /// - 지정된 키가 있는 경우 로컬에서 활성화된 것으로 간주한다.
        private bool UseCommandInLocal => PlayerPrefs.HasKey(UseCommandKey);

        /// <summary>
        /// 커맨트 로컬 활성화 상태 설정
        /// </summary>
        public void SetCommandEnable(bool enable)
        {
            if (enable)
            {
                // 혹시 사용할 일이 있을지 모르지만... 값은 활성화 시간으로...
                DateTime now = DateTime.Now;
                PlayerPrefs.SetString(UseCommandKey, now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                PlayerPrefs.DeleteKey(UseCommandKey);
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