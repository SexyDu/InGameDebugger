namespace SexyDu.InGameDebugger
{
    public interface IActivable
    {
        public void Activate();
    }

    public interface IClearable
    {
        public void Clear();
    }

    public interface IDestroyable
    {
        public void Destroy();
    }
}