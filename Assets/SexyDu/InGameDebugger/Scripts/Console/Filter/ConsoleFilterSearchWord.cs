namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 콘솔 검색어 필터 항목
    /// </summary>
    public class ConsoleFilterSearchWord : ConsoleFilter
    {
        /// <summary>
        /// 필터 여부 반환
        /// : IConsoleFilter
        /// </summary>
        public override bool IsFiltered(ILogMessage message)
        {
            // 검색어가 존재하는 상황에서 condition에 검색어가 포함되어 있지 않은 경우
            if (HasSearchWord)
                return !message.Condition.Contains(searchWord);
            else
                return false;
        }

        // 검색어
        private string searchWord = string.Empty;

        // 검색어
        public string SearchWord { get => searchWord; }
        /// 검색어 존재 여부
        public bool HasSearchWord { get => !string.IsNullOrEmpty(searchWord); }

        /// <summary>
        /// 검색어 설정
        /// </summary>
        public void SetSearchWord(string searchWord)
        {
            if (!this.searchWord.Equals(searchWord))
            {
                this.searchWord = searchWord;

                NotifyChanged();
            }
        }
    }
}