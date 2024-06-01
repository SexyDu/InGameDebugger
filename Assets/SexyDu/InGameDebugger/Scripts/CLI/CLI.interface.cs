using System;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// CLI 실행 인터페이스
    /// </summary>
    public interface ICommandLineInterface
    {
        /// <summary>
        /// 명령 실행
        /// </summary>
        public void Execute(string commandLine);

        /// <summary>
        /// 키워드가 포함된 명령어 배열 반환
        /// </summary>
        public string[] GetMatchedCommands(string keyword);
    }
    
    /// <summary>
    /// CLI 명령어 딕셔너리 인터페이스
    /// </summary>
    public interface ICommandDictionary
    {
        /// <summary>
        /// 명령 추가
        /// </summary>
        public void Bind(string command, Action<string[]> execute);

        /// <summary>
        /// 명령 제거
        /// </summary>
        public void Unbind(string command);
    }
}