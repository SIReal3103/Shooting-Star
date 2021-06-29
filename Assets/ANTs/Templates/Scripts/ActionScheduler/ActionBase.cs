using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace ANTs.Template
{
    [Serializable]
    public enum ActionType
    {
        TriggerWithSync,
        TriggerButNotSync,
        Boolean,
        Custom
    }

    [RequireComponent(typeof(ActionScheduler))]
    public abstract class ActionBase : MonoBehaviour, IAction
    {
        public event Action OnActionStartEvent;
        public event Action OnActionStopEvent;

        [HideInInspector] [SerializeField] ActionType typeOfAction;
        [HideInInspector] [SerializeField] bool isActionActive = false;
        [HideInInspector] [SerializeField] bool actionStartOnPlay;
        [HideInInspector] [SerializeField] bool isAttachWithAnimator = false;
        [HideInInspector] [SerializeField] bool isTransitionTrigger;
        [HideInInspector] [SerializeField] bool syncWithAnimation;       

        const float EXIT_TIME_OFFSET = 0.05f;
        const float TRANSITION_WAIT_TIME = 0.1f; // Must larger than ENTERED_TIME_OFFSET

        protected AnimatorEvents animatorEvents;
        protected Animator animator;

        private ActionScheduler scheduler;
        private float transitionTimer;

        public bool IsActionActive { get => isActionActive; }

        protected virtual void Awake()
        {
            animatorEvents = GetComponentInChildren<AnimatorEvents>();
            
            scheduler = GetComponent<ActionScheduler>();

            if (isAttachWithAnimator)
            {
                animator = GetComponentInChildren<Animator>();
                if(animator == null)
                    Debug.LogError("No animator found for " + GetType().Name);
            }
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

                if(syncWithAnimation)
                    SyncAnimationUpdate();
            }

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            transitionTimer += Time.deltaTime;
        }

        public virtual void ActionStart()
        {
            if (scheduler.IsPrevent(this))
            {
                Debug.LogWarning(GetType().Name + " is prevented!");
                return;
            }
            if (isTransitionTrigger) SetAnimatorTrigger();
            scheduler.StopActionRelavetiveTo(this);
            InitDataAndCallEvents();
        }

        private void InitDataAndCallEvents()
        {
            OnActionStartEvent?.Invoke();
            isActionActive = true;
            transitionTimer = 0f;
        }

        public virtual void ActionStop()
        {
            OnActionStopEvent?.Invoke();
            isActionActive = false;
            if (!isTransitionTrigger) SetAnimatorBool(false);
        }

        protected virtual void ActionUpdate() { }

        #region ===================================Animator control
        /// <summary>
        /// Only allow in BooleanNotStartOnPlay, BooleanStartOnPlay 
        /// or Custom with isTransitionTrigger on option in ActionType.
        /// </summary>
        /// <param name="value"></param>       
        private void SyncAnimationUpdate()
        {            
            if (isAttachWithAnimator)
            {
                if (transitionTimer < TRANSITION_WAIT_TIME) return;
                if (AnimatorTransitionTime() < EXIT_TIME_OFFSET) ActionStop();
            }
        }

        private float AnimatorTransitionTime()
        {
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        protected void SetAnimatorBool(bool value)
        {
            if (isAttachWithAnimator)
            {
                Assert.IsNotNull(animator);
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
            if (isAttachWithAnimator && isTransitionTrigger)
            {
                Assert.IsNotNull(animator);
                animator.SetTrigger(GetType().Name);
            }
        }
        #endregion
    }
}