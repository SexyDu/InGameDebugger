using System;

namespace SexyDu.InGameDebugger
{
    public interface IFramerateSubject : IDisposable
    {
        public void Subscribe(IFramerateObserver observer);
        public void Unsubscribe(IFramerateObserver observer);
    }
}