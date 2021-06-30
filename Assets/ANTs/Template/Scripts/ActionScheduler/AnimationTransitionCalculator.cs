using System.Collections;
using UnityEngine;

namespace ANTs.Template
{
    public class AnimationTransitionCalculator
    {
        public event System.Action OnTransitionExitEvent;

        const float ENTER_TIME_OFFSET = 0.1f;
        const float TRANSITION_WAIT_TIME = 0.1f;

        private float transitionTimer;
        private bool isTransitionEntered;
        Animator animator;

        public AnimationTransitionCalculator(Animator animator)
        {
            this.animator = animator;
            transitionTimer = 0f;
            isTransitionEntered = false;
        }

        public IEnumerator FixedUpdate()
        {
            while (true)
            {
                if (transitionTimer > TRANSITION_WAIT_TIME)
                {
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

                transitionTimer += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        private float AnimatorNormalizedTime()
        {
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }
}