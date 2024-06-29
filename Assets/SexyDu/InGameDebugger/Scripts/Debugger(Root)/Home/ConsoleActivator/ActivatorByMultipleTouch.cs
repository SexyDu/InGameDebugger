using UnityEngine;
using SexyDu.ContainerSystem;
using SexyDu.Tool;

namespace SexyDu.InGameDebugger
{
    /// <summary>
    /// 다중터치를 활용한 콘솔 활성화 기능
    /// * ActivateTouchCount(수)의 터치 입력이 ActivateTouchTime(초) 유지 시 콘솔 활성화
    /// </summary>
    public class ActivatorByMultipleTouch : IConsoleActivator, IOnFrameTarget
    {
        public ActivatorByMultipleTouch()
        {
            // 도커에 IOnFrameContainer가 없는 경우 추가
            if (!ContainerDocker.Has<IOnFrameContainer>())
                ContainerDocker.Dock<IOnFrameContainer>(new OnFrameContainer());
        }

        public ActivatorByMultipleTouch(IActivable activable)
        {
            this.activable = activable;

            // 도커에 IOnFrameContainer가 없는 경우 추가
            if (!ContainerDocker.Has<IOnFrameContainer>())
                ContainerDocker.Dock<IOnFrameContainer>(new OnFrameContainer());
        }

        /// <summary>
        /// 활성화 대상
        /// </summary>
        private IActivable activable = null;

        /// <summary>
        /// 활성화 대상 성정
        /// </summary>
        public IConsoleActivator Set(IActivable activable)
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

        private const int ActivateTouchCount = 5; // 활성화 터치 수
        private const float ActivateTouchTime = 0.5f; // 활성화 터치 유지 시간
        private float initialReadyTime = float.MinValue; // 활성화 터치 최초 확인 시간(Time.time)
        private bool ReadyForActivation => initialReadyTime != float.MinValue; // 활성화 대기 상태 여부

        private bool TouchStateForActivation =>
            Input.touchCount.Equals(ActivateTouchCount) // 현재 터치 입력수가 활성화 터치 입력 수와 동일한 경우
#if UNITY_EDITOR
            || Input.GetKey(ActivateKeyForEditor)
#endif
            ;

#if UNITY_EDITOR
        private const KeyCode ActivateKeyForEditor = KeyCode.BackQuote;
#endif

        /// <summary>
        /// 활성화 대기 상태 설정
        /// </summary>
        private void SetReadyForActivation(bool active)
        {
            if (active)
                initialReadyTime = Time.time;
            else
                initialReadyTime = float.MinValue;
        }
    }
}