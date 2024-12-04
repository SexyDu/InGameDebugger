namespace SexyDu.InGameDebugger
{
    public interface IFramerateObserver
    {
        public void OnReceivedFrameRate(float framerate);
    }
}