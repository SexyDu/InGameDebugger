using System;
using UnityEngine;
using TMPro;
using SexyDu.UGUI;

namespace SexyDu.InGameDebugger.View
{
    /// <summary>
    /// 검색어 필터 UI 관리 클래스
    /// </summary>
    public class FilterItemSearchWord : ConsoleFilterItem
    {
        // 검색어 필터
        private ConsoleFilterSearchWord filter = null;
        public override IConsoleFilter Filter { get => filter; }

        // 입력된 검색어 표시 구조
        [SerializeField] private SearchWordDisplay display;

        public FilterItemSearchWord Initialize()
        {
            filter = new ConsoleFilterSearchWord();

            return this;
        }

        /// <summary>
        /// 검색어 설정
        /// </summary>
        private void SetSearchWord(string word)
        {
            filter.SetSearchWord(word);

            display.SetSearchWord(word);
        }

        #region Popup
        [Header("Popup")]
        // 팝업의 부모가 될 Transform
        [SerializeField] private Transform popupParent;
        // 팝업 경로
        private const string PopupPath = "IGDPrefabs/Popups/PopupFilterSearchWord";
        /// <summary>
        /// 검색 팝업 활성화
        /// </summary>
        public void OnClickActivate()
        {
            // 검색 팝업 생성
            CreatePopup();
        }

#if true
        /// <summary>
        /// 검색 팝업 생성
        /// </summary>
        private void CreatePopup()
        {
            PopupInputField popup = ResourcePopup.Load<PopupInputField>(PopupPath, popupParent);

            popup.Initialize(filter.SearchWord)
                .SelectInputField()
                .CallbackOnDecided(SetSearchWord);
        }
#else
        /// <summary>
        /// 검색 팝업 생성
        /// </summary>
        private void CreatePopup()
        {
            PopupInputField source = Resources.Load<PopupInputField>(PopupPath);

            if (source == null)
                throw new NullReferenceException($"지정된 경로({PopupPath})에서 PopupInputField 프리팹을 찾을 수 없습니다.");
            else
                CreatePopup(source);
        }

        /// <summary>
        /// 검색 팝업 생성
        /// </summary>
        private void CreatePopup(PopupInputField source)
        {
            PopupInputField popup = Instantiate(source, popupParent);

            popup.Initialize(filter.SearchWord)
                .SelectInputField()
                .CallbackOnDecided(SetSearchWord);
        }
#endif
        #endregion

        /// <summary>
        /// 입력된 검색어 표출 구조체
        /// </summary>
        [Serializable]
        public struct SearchWordDisplay
        {
            [SerializeField] private GameObject gameObject;
            [SerializeField] private TMP_Text text;

            /// <summary>
            /// 검색어 설정
            /// </summary>
            public void SetSearchWord(string word)
            {
                text.SetText(word);

                gameObject.SetActive(!string.IsNullOrEmpty(word));
            }
        }
    }
}