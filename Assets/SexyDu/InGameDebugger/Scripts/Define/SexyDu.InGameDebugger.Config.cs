#define TESTING_SAMPLE

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

            // CLI 터미널 생성
            terminal = new Terminal();

            if (!Settings.UseCLI)
                Debug.LogWarning("[InGameDebugger] CLI 상태가 비활성화 되어 있어습니다.\n 활성화 전까지 Command 추가가 되지 않습니다.");
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

#if TESTING_SAMPLE
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
#endif
        }

        /// <summary>
        /// 디버거 생성
        /// </summary>
        public Debugger CreateDebugger()
        {
            if (debugger == null)
            {
                Debugger source = Resources.Load<Debugger>(Settings.DebuggerPath);

                if (source != null)
                {
                    debugger = MonoBehaviour.Instantiate(source);
                    debugger.Initialize();
                }
                else
                    throw new NullReferenceException($"해당 경로에서 Debugger를 찾을 수 없습니다. path : {Settings.DebuggerPath}");
            }
            else
                Debug.LogWarningFormat("이미 Debugger({0})가 존재합니다.", debugger.name);

            return debugger;
        }
        #endregion

        #region CLI
        private Terminal terminal = null;

        public ICommandLineInterface CLI => terminal;
        public ICommandContainer CommandContainer => terminal;
        #endregion
    }
}