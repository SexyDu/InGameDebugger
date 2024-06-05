namespace SexyDu.InGameDebugger
{
    public interface IConsoleHandlerUI
    {
        /// <summary>
        /// UI 갱신
        /// </summary>
        public void Refresh();

        /// <summary>
        /// 플레이 상태 설정
        /// </summary>
        public void SetPlayStatus(bool playing);
    }
}