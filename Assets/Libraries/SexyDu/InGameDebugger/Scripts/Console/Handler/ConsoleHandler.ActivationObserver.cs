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
        public void Unsubscribe(IConsoleActivationObserver observer)
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

    /// <summary>
    /// 활성화 상태 변경 옵저버
    /// </summary>
    public interface IConsoleActivationObserver
    {
        public void OnConsoleActivationChanged(bool active);
    }

    /// <summary>
    /// 활성화 상태 변경 서브젝트
    /// </summary>
    public interface IConsoleActivationSubject
    {
        public void Subscribe(IConsoleActivationObserver observer);
        public void Unsubscribe(IConsoleActivationObserver observer);
    }
}