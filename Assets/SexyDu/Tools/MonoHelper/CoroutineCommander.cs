using System;
using System.Collections;
using UnityEngine;

namespace SexyDu.Tool
{
    public class CoroutineCommander : IDisposable
    {
        private readonly IEnumerator enumerator = null;

        public CoroutineCommander(IEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        /// <summary>
        /// 코루틴 수행
        /// </summary>
        private IEnumerator Routine()
        {
            yield return enumerator;

            onCompleted?.Invoke();
            Clear();
        }

        // 코루틴 수행 워커
        private MonoBehaviour worker = null;
        // 현재 수행중인 코루틴
        private Coroutine coroutine = null;
        // 동작 중 여부
        public bool IsRunning => coroutine != null;

        /// <summary>
        /// 코루틴 실행
        /// </summary>
        public CoroutineCommander Run(MonoBehaviour worker)
        {
            this.worker = worker;
            this.coroutine = this.worker.StartCoroutine(Routine());

            return this;
        }

        /// <summary>
        /// 코루틴 취소
        /// </summary>
        public void Cancel()
        {
            if (coroutine != null)
            {
                worker.StopCoroutine(coroutine);
                Clear();
            }
        }

        /// <summary>
        /// 클리어
        /// </summary>
        private void Clear()
        {
            coroutine = null;
            worker = null;
        }

        // 코루틴 완료 콜백
        private Action onCompleted = null;
        /// <summary>
        /// 코루틴 완료 콜백 등록
        /// </summary>
        public CoroutineCommander Subscribe(Action onCompleted)
        {
            this.onCompleted = onCompleted;

            return this;
        }

        public void Dispose()
        {
            Cancel();

            onCompleted = null;
        }
    }
}