using System;
using UnityEngine;
using TMPro;
using SexyDu.UGUI;

namespace SexyDu.InGameDebugger.View
{
    public class PopupTerminal : ResourcePopup
    {
        public const string ResourcePath = "IGDPrefabs/Popups/PopupTerminal";

        // Input field
        [SerializeField] private TMP_InputField inputField;
        // 설정된 워드
        private string text {
            get => inputField.text;
            set => inputField.text = value;
        }
        // CLI
        private ICommandLineInterface CLI => InGameDebuggerConfig.Ins.CLI;

        #region setter
        /// <summary>
        /// 초기 설정
        /// </summary>
        public PopupTerminal Initialize()
        {
            inputField.onValueChanged.AddListener(OnInputFieldChanged);

            // 자동완성 초기 설정
            autoComplete.Initialize(OnWordCompletionSelected);

            return this;
        }

        /// <summary>
        /// InputField 선택
        /// </summary>
        public PopupTerminal SelectInputField()
        {
            inputField.Select();
            inputField.caretPosition = text.Length;

            return this;
        }

        /// <summary>
        /// 팝업 종료 이벤트
        /// </summary>
        private Action onClosed = null;
        public PopupTerminal CallbackOnClosed(Action onClosed)
        {
            this.onClosed = onClosed;

            return this;
        }

        /// <summary>
        /// input field 값 변경 이벤트
        /// </summary>
        private void OnInputFieldChanged(string text)
        {
            // 자동완성 문자열 가져오고 설정
            string[] wordCompletions = string.IsNullOrEmpty(text) ? null : CLI.GetMatchedCommands(text);
            autoComplete.SetWordCompletions(wordCompletions);
        }
        #endregion

        #region feature
        /// <summary>
        /// 현재 입력된 커맨드 실행
        /// </summary>
        private void Execute()
        {
            Execute(text);
        }

        /// <summary>
        /// 커맨드 실행
        /// </summary>
        private void Execute(string text)
        {
            CLI.Execute(text);
        }

        /// <summary>
        /// 팝업 클리어
        /// </summary>
        private void Clear()
        {
            inputField.onValueChanged.RemoveAllListeners();

            autoComplete.Clear();
        }

        /// <summary>
        /// 입력 필드 클리어
        /// </summary>
        private void ClearInputField()
        {
            text = string.Empty;
        }

        /// <summary>
        /// 팝업 종료
        /// </summary>
        public void Close()
        {
            onClosed?.Invoke();

            Destroy(gameObject);
            Destroy(this);
        }
        #endregion

        #region event on click
        /// <summary>
        /// 실행
        /// </summary>
        public void OnClickExecute()
        {
            Execute();

            ClearInputField();
        }

        /// <summary>
        /// 클리어 버튼 클릭
        /// </summary>
        public void OnClickClearInputField()
        {
            ClearInputField();
        }

        /// <summary>
        /// 취소 버튼 클릭
        /// </summary>
        public void OnClickClose()
        {
            Clear();

            Close();
        }
        #endregion

        #region AutoComplete
        // 자동완성 기능
        [SerializeField] private TerminalAutoComplete autoComplete;

        /// <summary>
        /// 자동 완성 단어 선택 시 이벤트
        /// </summary>
        private void OnWordCompletionSelected(string word)
        {
            text = word;

            SelectInputField();
        }
        #endregion
    }
}