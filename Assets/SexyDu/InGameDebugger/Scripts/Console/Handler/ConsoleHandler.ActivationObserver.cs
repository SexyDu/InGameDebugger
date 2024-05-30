using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public abstract partial class ConsoleHandler : IConsoleActivationSubject
    {
        private List<IConsoleActivationObserver> activationObservers = new List<IConsoleActivationObserver>();

        /// <summary>
        /// 활성화 옵저버 등록
        /// </summary>
        public void Subscribe(IConsoleActivationObserver observer)
        {
            if (activationObservers.Contains(observer))
            {
                Debug.LogWarningFormat("이미 존재하는 옵저버가 등록을 시도했습니다. Type : {0}", observer.GetType());
            }
            else
                activationObservers.Add(observer);
        }

        /// <summary>
        /// 활성화 옵저버 해제
        /// </summary>
        public void Unsubsctibe(IConsoleActivationObserver observer)
        {
            activationObservers.Remove(observer);
        }

        /// <summary>
        /// 활성화 알림
        /// </summary>
        private void NotifyActivation()
        {
            NotifyActivation(GameObjectCache.activeSelf);
        }

        /// <summary>
        /// 활성화 알림
        /// </summary>
        private void NotifyActivation(bool active)
        {
            for (int i = 0; i < activationObservers.Count; i++)
            {
                activationObservers[i].OnConsoleActivationChanged(active);
            }
        }
    }

    public interface IConsoleActivationObserver
    {
        public void OnConsoleActivationChanged(bool active);
    }

    public interface IConsoleActivationSubject
    {
        public void Subscribe(IConsoleActivationObserver observer);
        public void Unsubsctibe(IConsoleActivationObserver observer);
    }
}