using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace ANTs.Template
{
    [Serializable]
    public enum ActionType
    {
        SynWithAnimation,
        TriggerButNotSync,
        BooleanNotStartOnPlay,
        BooleanStartOnPlay,
        Custom
    }

    [RequireComponent(typeof(ActionScheduler))]
    public abstract class ActionBase : MonoBehaviour, IAction
    {   
        public event Action OnActionStartEvent;
        public event Action OnActionStopEvent;

        [HideInInspector] [SerializeField] bool isActionStart = false;
        [HideInInspector] [SerializeField] bool isTransitionTrigger;
        [HideInInspector] [SerializeField] bool syncWithAnimation;
        [HideInInspector] [SerializeField] bool actionStartOnPlay;
        [HideInInspector] [SerializeField] ActionType typeOfAction;

        const float ENTERED_TIME_OFFSET = 0.1f;

        protected AnimatorEvents animatorEvents;
        protected Animator animator;

        private bool isAnimationEntered;
        private ActionScheduler scheduler;

        public bool IsActionStart { get => isActionStart; set => isActionStart = value; }

        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            animatorEvents = GetComponentInChildren<AnimatorEvents>();
            scheduler = GetComponent<ActionScheduler>();
        }

        protected virtual void Start()
        {
            if (actionStartOnPlay)
                ActionStart();
        }

        protected void Update()
        {
            if (IsActionStart)
            {
                ActionUpdate();
                if (syncWithAnimation)
                {
                    float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    if (isAnimationEntered)
                    {
                        if (time < ENTERED_TIME_OFFSET)
                        {
                            Debug.Log("Animation Exit");
                            ActionStop();
                            return;
                        }
                    }
                    else if (time > ENTERED_TIME_OFFSET)
                    {
                        Debug.Log("Animation Entered");
                        isAnimationEntered = true;
                    }
                }
            }
        }

        public virtual void ActionStart()
        {
            if (scheduler.IsActionPrevent(this)) return;

            isAnimationEntered = false;
            isActionStart = true;
            OnActionStartEvent?.Invoke();

            if (isTransitionTrigger) SetTrigger();

            scheduler.StopActionRelavetiveTo(this);
        }

        public virtual void ActionStop()
        {
            isActionStart = false;
            OnActionStopEvent?.Invoke();
        }

        protected virtual void ActionUpdate() { }

        #region ===================================Animator control
        protected void SetAnimationBool(bool value)
        {
            Assert.AreEqual(isTransitionTrigger, false);
            if (animator) animator.SetBool("Is" + GetType().Name, value);
        }

        private void SetTrigger()
        {
            Assert.AreEqual(isTransitionTrigger, true);
            if (animator) animator.SetTrigger(GetType().Name);
        }
        #endregion
    }
}