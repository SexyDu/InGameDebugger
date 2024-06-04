using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SexyDu.InGameDebugger.View
{
    /// <summary>
    /// 커맨드 자동완성
    /// </summary>
    public partial class TerminalAutoComplete : MonoBehaviour, IClearable
    {
        /// <summary>
        /// 초기 설정
        /// </summary>
        /// <param name="onWordSelected"></param>
        /// <returns></returns>
        public TerminalAutoComplete Initialize(Action<string> onWordSelected)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Initialize(onWordSelected);
            }

            return this;
        }

        /// <summary>
        /// 클리어
        /// : IClearable
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Clear();
            }
        }

        /// <summary>
        /// 자동완성 단어 설정
        /// </summary>
        /// <param name="wordCompletions"></param>
        public void SetWordCompletions(string[] wordCompletions)
        {
            bool active = wordCompletions != null && wordCompletions.Length > 0;

            SetActive(active);

            if (active)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (i < wordCompletions.Length)
                        items[i].Set(wordCompletions[i]);
                    else
                        items[i].Inactivate();
                }
            }
        }

        #region WordCompletion
        /// <summary>
        /// 자동완성 아이템
        /// </summary>
        [SerializeField] private WordCompletionItem[] items;
        #endregion

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private GameObject gameObjectCache;
        private GameObject GameObjectCache { get => gameObjectCache; }

        private void SetActive(bool active)
        {
            GameObjectCache.SetActive(active);
        }
        #endregion

        [Serializable]
        public struct WordCompletionItem : IClearable, IInactivable
        {
            [SerializeField] private GameObject gameObject;
            [SerializeField] private Button button; // 이벤트를 연결할 Button
            [SerializeField] private TMP_Text textMesh; // 자동완성 단어 텍스트

            /// <summary>
            /// 사용하기에 완전한 상태
            /// </summary>
            public bool Absolute =>
                gameObject != null
                && button != null
                && textMesh != null
                ;

#if UNITY_EDITOR
            /// <summary>
            /// 에디터 설정을 위해 사용되는 생성자
            /// </summary>
            public WordCompletionItem(GameObject gameObject, Button button, TMP_Text textMesh)
            {
                this.gameObject = gameObject;
                this.button = button;
                this.textMesh = textMesh;

                this.onSelected = null;
            }
#endif

            /// <summary>
            /// 초기 설정
            /// </summary>
            public void Initialize(Action<string> onSelected)
            {
                this.onSelected = onSelected;

                button.onClick.AddListener(OnClick);
            }

            /// <summary>
            /// 자동완성 단어 설정
            /// </summary>
            public void Set(string word)
            {
                textMesh.SetText(word);

                SetActive(!string.IsNullOrEmpty(textMesh.text));
            }

            /// <summary>
            /// 클리어
            /// </summary>
            public void Clear()
            {
                button.onClick.RemoveAllListeners();
                onSelected = null;
            }

            /// <summary>
            /// 비활성화
            /// </summary>
            public void Inactivate()
            {
                Set(string.Empty);
            }

            /// <summary>
            /// 활성화 상태 설정
            /// </summary>
            private void SetActive(bool active)
            {
                gameObject.SetActive(active);
            }

            // 선택 이벤트
            private Action<string> onSelected;

            /// <summary>
            /// 자동완성 아이템 클릭 이벤트
            /// </summary>
            private void OnClick()
            {
                onSelected?.Invoke(textMesh.text);
            }
        }

        /// [Only editor]
#if UNITY_EDITOR
        [Header("Editor")]
        /// WordCompletionItem의 부모 Transform
        [SerializeField] private Transform parentOfWordCompletionItems;

        /// <summary>
        /// [Editor] parentOfWordCompletionItems의 Childs에서 WordCompletionItem을 찾아 설정
        /// </summary>
        public void SetWordCompletionItems()
        {
            List<WordCompletionItem> list = new List<WordCompletionItem>();

            for (int i = 0; i < parentOfWordCompletionItems.childCount; i++)
            {
                WordCompletionItem item = GetWordCompletionItem(parentOfWordCompletionItems.GetChild(i));
                if (item.Absolute)
                    list.Add(item);
            }

            items = list.ToArray();
        }

        /// <summary>
        /// [Editor] Transform을 WordCompletionItem화 하여 반환
        /// </summary>
        public WordCompletionItem GetWordCompletionItem(Transform target)
        {
            return new WordCompletionItem(target.gameObject,
                target.GetComponentInChildren<Button>(), target.GetComponentInChildren<TMP_Text>());
        }
#endif
    }
}