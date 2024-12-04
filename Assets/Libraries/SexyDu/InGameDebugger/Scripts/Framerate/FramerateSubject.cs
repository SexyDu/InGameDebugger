using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SexyDu.Tool;

namespace SexyDu.InGameDebugger
{
    public class FramerateSubject : IFramerateSubject
    {
        public void Dispose()
        {
            if (coroutine != null)
            {
                coroutine.Dispose();
                coroutine = null;
            }

            observers.Clear();
        }

        public FramerateSubject(float delay = 0)
        {
            if (delay > 0)
                wait = new WaitForSeconds(delay);
        }

        private readonly List<IFramerateObserver> observers = new();

        public void Subscribe(IFramerateObserver observer)
        {
            if (observer != null)
                observers.Add(observer);

            if (!IsRunning || observers.Count > 0)
                Run();
        }

        public void Unsubscribe(IFramerateObserver observer)
        {
            if (observer != null)
                observers.Remove(observer);

            if (observers.Count == 0)
                Stop();
        }

        private void Distribute(float framerate)
        {
            foreach (var observer in observers)
                observer.OnReceivedFrameRate(framerate);
        }

        private IDisposable coroutine = null;
        private bool IsRunning => coroutine != null;
        private WaitForSeconds wait = null;

        private void Run()
        {
            Stop();

            coroutine = MonoHelper.StartCoroutine(CoFramerate());
        }

        private void Stop()
        {
            if (coroutine != null)
            {
                coroutine.Dispose();
                coroutine = null;
            }
        }

        private IEnumerator CoFramerate()
        {
            do
            {
                yield return wait;

                Distribute(1f / Time.deltaTime);
            } while (true);
        }
    }
}