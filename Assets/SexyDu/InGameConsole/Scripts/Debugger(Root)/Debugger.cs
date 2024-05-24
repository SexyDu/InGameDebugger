using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private bool onAwakeInit;
        private void Awake()
        {
            if (onAwakeInit)
                Initialize();
        }

        private Debugger Initialize()
        {
            InGameDebuggerConfig.Ins.Debugger = this;

            return this;
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

        #region ObjectCache
        [Header("ObjectCache")]
        [SerializeField] private Transform transformCache;
        public Transform TransformCache { get => transformCache; }
        #endregion
    }
}