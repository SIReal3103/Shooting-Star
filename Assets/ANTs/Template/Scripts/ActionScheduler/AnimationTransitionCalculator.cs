using System.Collections;
using UnityEngine;

namespace ANTs.Template
{
    public class AnimationTransitionCalculator
    {
        public event System.Action OnTransitionExitEvent;

        readonly private float ENTER_TIME_OFFSET = Time.maximumDeltaTime; // one frame

        private bool isTransitionEntered;
        private Animator animator;

        public AnimationTransitionCalculator(Animator animator)
        {
            this.animator = animator;
            isTransitionEntered = false;
        }

        public IEnumerator FixedUpdate()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (isTransitionEntered)
                {
                    if (AnimatorNormalizedTime() < ENTER_TIME_OFFSET)
                    {
                        OnTransitionExitEvent?.Invoke();
                        break;
                    }
                }
                else if (AnimatorNormalizedTime() > ENTER_TIME_OFFSET)
                {
                    isTransitionEntered = true;
                }
            }
        }

        private float AnimatorNormalizedTime()
        {
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }
}