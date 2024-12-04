using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SexyDu.InGameDebugger
{
    public abstract class MonoInDebuggerHome : MonoBehaviour
    {
        /// <summary>
        /// 초기 설정
        /// </summary>
        public abstract MonoInDebuggerHome Initialize();

        /// <summary>
        /// 클리어
        /// </summary>
        public abstract void Clear();
    }
}