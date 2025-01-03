using UnityEngine;

namespace SexyDu.Touch
{
    /// <summary>
    /// 멀티터치 위치 변경 조절 수행 핸들
    /// </summary>
    public class TransformPositionHandle : TransformHandle
    {
        private Vector2 previous = Vector2.zero;

        public override void Setting()
        {
            previous = body.Data.center;
        }

        public override Vector2 Process()
        {
            /// 여기선 직접 Target(Transform)을 건드리지 않고 deltaPosition만 반환한다
            Vector2 deltaPosition = (body.Data.center - previous) * UPPOP;
            previous = body.Data.center;

            return deltaPosition;
        }
    }
}