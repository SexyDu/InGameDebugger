using UnityEngine;
using SexyDu.ContainerSystem;
using SexyDu.Tool;

namespace SexyDu.InGameDebugger
{
    public class ActivatorByMultipleTouch : IActivator, IOnFrameTarget
    {
        public ActivatorByMultipleTouch()
        {
            if (!ContainerDocker.Has<IOnFrameContainer>())
                ContainerDocker.Dock<IOnFrameContainer>(new OnFrameContainer());
        }

        private IActivable activable = null;

        /// <summary>
        /// 활성화 대상 성정
        /// </summary>
        public IActivator Set(IActivable activable)
        {
            this.activable = activable;

            return this;
        }

        /// <summary>
        /// 활성화 기능 활성화 상태 설정
        /// </summary>
        public void SetEnableActivation(bool enable)
        {
            if (enable)
            {
                ContainerDocker.Bring<IOnFrameContainer>().Subscribe(this);
            }
            else
            {
                ContainerDocker.Bring<IOnFrameContainer>().Unsubscribe(this);

                SetReadyForActivation(false);
            }
        }

        /// <summary>
        /// 프레임 수행 함수
        /// </summary>
        public void OnFrame()
        {
            if (ReadyForActivation)
            {
                if (TouchStateForActivation)
                {
                    float pass = Time.time - initialReadyTime;
                    if (pass > ActivateTouchTime)
                    {
                        activable?.Activate();
                    }
                }
                else
                    SetReadyForActivation(false);
            }
            else
            {
                if (TouchStateForActivation)
                    SetReadyForActivation(true);
            }
        }

        private const int ActivateTouchCount = 5;
        private const float ActivateTouchTime = 0.5f;
        private float initialReadyTime = float.MinValue;
        private bool ReadyForActivation => initialReadyTime != float.MinValue;

        
#if UNITY_EDITOR
        private const KeyCode ActivateKeyForEditor = KeyCode.BackQuote;
        private bool TouchStateForActivation =>
            Input.touchCount.Equals(ActivateTouchCount) ||
            Input.GetKey(ActivateKeyForEditor);
#else
        private bool TouchStateForActivation => Input.touchCount.Equals(ActivateTouchCount);
#endif

        private void SetReadyForActivation(bool active)
        {
            if (active)
                initialReadyTime = Time.time;
            else
                initialReadyTime = float.MinValue;
        }
    }
}