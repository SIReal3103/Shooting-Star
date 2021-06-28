using System;
using UnityEngine;

namespace ANTs.Template
{
    [Serializable]
    public enum ActionType
    {
        TriggerWithSync,
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

        [HideInInspector] [SerializeField] bool isActionActive = false;
        [HideInInspector] [SerializeField] bool isTransitionTrigger;
        [HideInInspector] [SerializeField] bool syncWithAnimation;
        [HideInInspector] [SerializeField] bool actionStartOnPlay;
        [HideInInspector] [SerializeField] ActionType typeOfAction;

        const float ENTERED_TIME_OFFSET = 0.1f;

        protected AnimatorEvents animatorEvents;
        protected Animator animator;

        private bool isAnimationEntered;
        private ActionScheduler scheduler;

        public bool IsActionActive { get => isActionActive; set => isActionActive = value; }

        protected virtual void Awake()
        {
            animatorEvents = GetComponentInChildren<AnimatorEvents>();
            animator = GetComponentInChildren<Animator>();
            scheduler = GetComponent<ActionScheduler>();
        }

        protected virtual void Start()
        {
            if (actionStartOnPlay)
                ActionStart();
        }

        protected void Update()
        {
            if (IsActionActive)
            {
                ActionUpdate();
                if (syncWithAnimation)
                {
                    float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    if (isAnimationEntered)
                    {
                        if (time < ENTERED_TIME_OFFSET)
                        {
                            ActionStop();
                        }
                    }
                    else if (time > ENTERED_TIME_OFFSET)
                    {
                        isAnimationEntered = true;
                    }
                }
            }
        }

        public virtual void ActionStart()
        {
            if (scheduler.IsPrevent(this))
            {
                Debug.LogWarning(GetType().Name + " is prevented!");
                return;
            }
            InitData();
        }

        private void InitData()
        {
            isActionActive = true;
            OnActionStartEvent?.Invoke();
            scheduler.StopActionRelavetiveTo(this);
            isAnimationEntered = false;
            SetAnimatorTrigger();
        }

        public virtual void ActionStop()
        {
            isActionActive = false;
            OnActionStopEvent?.Invoke();
            if (!isTransitionTrigger) SetAnimatorBool(false);
        }

        protected virtual void ActionUpdate() { }

        #region ===================================Animator control
        /// <summary>
        /// Only allow in BooleanNotStartOnPlay, BooleanStartOnPlay or Custom with isTransitionTrigger on.
        /// </summary>
        /// <param name="value"></param>        
        protected void SetAnimatorBool(bool value)
        {
            if (animator)
            {
                if (isTransitionTrigger)
                {
                    Debug.LogWarning("SetAnimationBool function shouldn't be called by " + 
                        GetType().Name + 
                        " which set isTransitionTrigger true"
                    );
                    return;
                }
                animator.SetBool("Is" + GetType().Name, value);
            }
        }
        private void SetAnimatorTrigger()
        {
            if (animator)
            {
                if (!isTransitionTrigger)
                {
                    Debug.LogWarning("SetAnimatorTrigger function shouldn't be called by " +
                        GetType().Name +
                        " which set isTransitionTrigger false"
                    );
                    return;
                }
                animator.SetTrigger(GetType().Name);
            }
        }
        #endregion
    }
}