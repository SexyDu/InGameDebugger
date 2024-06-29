namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 콘솔 활성화 기능 인터페이스
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// 활성화 대상 성정
        /// </summary>
        public IActivator Set(IActivable activable);

        /// <summary>
        /// 활성화 기능 활성화 상태 설정
        /// </summary>
        public void SetEnableActivation(bool enable);
    }
}