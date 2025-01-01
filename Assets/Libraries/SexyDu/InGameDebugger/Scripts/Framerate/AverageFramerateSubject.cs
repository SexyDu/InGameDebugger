using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SexyDu.Tool;

namespace SexyDu.InGameDebugger
{
    public class AverageFramerateSubject : IFramerateSubject
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

        public AverageFramerateSubject(float delay = 0)
        {
            this.delay = delay;
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
        private float delay = 0;

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
            float pass = 0;
            int count = 0;
            
            do
            {
                yield return null;

                pass += Time.deltaTime;
                count++;

                if (pass > delay)
                {
                    Distribute(1f / (pass / (float)count));

                    pass = 0;
                    count = 0;
                }
            } while (true);
        }
    }
}