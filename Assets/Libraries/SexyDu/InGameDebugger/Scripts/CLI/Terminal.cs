using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 명령어 처리 터미널
    /// </summary>
    public class Terminal : ICommandContainer, ICommandLineInterface
    {
        // 명령 딕셔너리
        private Dictionary<string, Action<string[]>> commands;

        private InGameDebuggerSettings Settings => InGameDebuggerConfig.Ins.Settings;

        public Terminal()
        {
            commands = new Dictionary<string, Action<string[]>>();
        }

        /// <summary>
        /// 명령 추가
        /// : ICommandDictionary
        /// </summary>
        public void Bind(string command, Action<string[]> execute)
        {
            if (commands.ContainsKey(command))
                Debug.LogWarningFormat("명렁어 '{0}'가 이미 정의되어 Command에 추가할 수 없습니다.", command);
            else if (Settings.UseCLI)
                commands.Add(command, execute);
        }

        /// <summary>
        /// 명령 제거
        /// : ICommandDictionary
        /// </summary>
        public void Unbind(string command)
        {
            commands.Remove(command);
        }

        /// <summary>
        /// 명령 실행
        /// : ICommandLineInterface
        /// </summary>
        public void Execute(string commandLine)
        {
            DisassembleCommandLine disassemble = new DisassembleCommandLine(commandLine);

            if (disassemble.Exceptional)
            {
                Debug.LogErrorFormat("비정상적인 명령입니다.\n -CommandLine : {0}"
                    , commandLine);
            }
            else if (commands.ContainsKey(disassemble.Command))
            {
                commands[disassemble.Command].Invoke(disassemble.Parameters);
            }
            else
            {
                Debug.LogErrorFormat("명령어 '{0}'를 찾을 수 없습니다.\n -CommandLine : {1}"
                    , disassemble.Command, commandLine);
            }
        }

        /// <summary>
        /// 키워드가 포함된 명령어 검색
        /// : ICommandLineInterface
        /// </summary>
        public string[] GetMatchedCommands(string keyword)
        {
            string patternCore = $"{keyword.ToLower()}.*";
            string patternContain = $".*{keyword.ToLower()}.*";

            int coreIndex = 0;
            List<string> list = new List<string>();

            foreach (string cmd in commands.Keys)
            {
                // 패턴 비교 문자열
                string compare = cmd.ToLower();

                if (Regex.IsMatch(compare, patternCore))
                {
                    list.Insert(coreIndex++, cmd);
                }
                else if (Regex.IsMatch(compare, patternContain))
                {
                    list.Add(cmd);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// CommandLine 분해 구조
        /// </summary>
        public struct DisassembleCommandLine
        {
            // 명령어
            private readonly string command;
            public string Command => command;

            // 인자 배열
            private readonly string[] parameters;
            public string[] Parameters => parameters;

            // 예외상황
            public bool Exceptional => string.IsNullOrEmpty(command);

            public DisassembleCommandLine(string commandLine)
            {
                string[] disassemble = commandLine.Split(' ');

                string command = string.Empty;
                string[] parameters = null;

                if (disassemble != null && disassemble.Length > 0)
                {
                    command = disassemble[0];

                    if (disassemble.Length > 1)
                    {
                        parameters = new string[disassemble.Length - 1];
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i] = disassemble[i + 1];
                        }
                    }
                }

                this.command = command;
                this.parameters = parameters;
            }
        }
    }
}