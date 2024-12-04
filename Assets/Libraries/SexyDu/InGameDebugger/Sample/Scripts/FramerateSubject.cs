using System;
using System.Collections;
using UnityEngine;
using SexyDu.Tool;

namespace SexyDu.InGameDebugger.Sample
{
    public class FramerateSubject
    {
        IDisposable co = null;
        public void Run(IFramerateObserver observer, float delay = 0)
        {
            if (observer != null)
            {
                Stop();

                co = MonoHelper.StartCoroutine(IeFramerate(observer, delay > 0 ? new WaitForSeconds(delay) : null));
            }
        }

        public void Stop()
        {
            if (co != null)
            {
                co.Dispose();
                co = null;
            }
        }

        private IEnumerator IeFramerate(IFramerateObserver observer, YieldInstruction yieldInstruction)
        {
            do
            {
                yield return yieldInstruction;

                observer.OnReceivedFrameRate(1f / Time.deltaTime);
            } while (true);
        }
    }

    public interface IFramerateObserver
    {
        public void OnReceivedFrameRate(float framerate);
    }
}