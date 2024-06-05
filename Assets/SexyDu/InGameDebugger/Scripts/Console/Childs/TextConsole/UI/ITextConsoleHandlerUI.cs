namespace SexyDu.InGameDebugger
{
    public interface ITextConsoleHandlerUI : IConsoleHandlerUI
    {
        public void SetStackTraceStatus(bool enable);
    }
}