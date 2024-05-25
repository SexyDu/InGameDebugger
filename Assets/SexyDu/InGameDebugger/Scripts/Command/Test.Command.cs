using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class TestCommand : MonoBehaviour
    {
        private Dictionary<string, Action<string>> cmdDictionary = new Dictionary<string, Action<string>>();

        public void Add(string cmd, Action<string> act)
        {
            if (cmdDictionary.ContainsKey(cmd))
                Debug.LogWarningFormat($"Command '{cmd}'가 이미 정의되어 있습니다.");
            else
                cmdDictionary.Add(cmd, act);
        }

        public void Remove(string cmd)
        {
            cmdDictionary.Remove(cmd);
        }

        public string[] GetMatchedCommands(string keyword)
        {
            string patternCore = $"{keyword.ToLower()}.*";
            string patternContain = $".*{keyword.ToLower()}.*";

            int coreIndex = 0;
            List<string> list = new List<string>();

            foreach (string cmd in cmdDictionary.Keys)
            {
                if (Regex.IsMatch(cmd, patternCore))
                {
                    list.Insert(coreIndex++, cmd);
                }
                else if (Regex.IsMatch(cmd, patternContain))
                {
                    list.Add(cmd);
                }
            }

            return list.ToArray();
        }
    }
}