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

        protected AnimatorEvents animatorEvents;
        protected Animator animator;

        private AnimationTransitionCalculator calculator;
        private ActionScheduler scheduler;

        public bool IsActionActive { get => isActionActive; }

        protected virtual void Awake()
        {
            animatorEvents = GetComponentInChildren<AnimatorEvents>();
            scheduler = GetComponent<ActionScheduler>();

            if (isAttachWithAnimator)
            {
                animator = GetComponentInChildren<Animator>();
                if (animator == null)
                    Debug.LogError("No animator found for " + this);
            }
        }

        protected virtual void Start()
        {
            if (actionStartOnPlay)
                ActionStart();
        }

        protected virtual void Update()
        {
            if (IsActionActive)
            {
                ActionUpdate();
            }
        }

        #region =================================== Overrideable
        public virtual void ActionStart()
        {
            if (scheduler.IsPrevent(this))
            {
                Debug.LogWarning(GetType().Name + " is prevented!");
                return;
            }

            InitDataAndCall();

            if (syncWithAnimation)
                InitSyncWithAnimationLogic();
        }

        private void InitDataAndCall()
        {
            scheduler.StopActionRelavetiveTo(this);
            if (isTransitionTrigger) SetAnimatorTrigger();
            OnActionStartEvent?.Invoke();
            isActionActive = true;
        }
        public virtual void ActionStop()
        {
            OnActionStopEvent?.Invoke();
            isActionActive = false;
            if (!isTransitionTrigger) SetAnimatorBool(false);
        }

        protected virtual void ActionUpdate() { }
        #endregion

        #region ====================================== Sync Animation Logics
        private void InitSyncWithAnimationLogic()
        {
            if (isAttachWithAnimator)
            {
                calculator = new AnimationTransitionCalculator(animator);
                calculator.OnTransitionExitEvent += ActionStop;
                StartCoroutine(calculator.FixedUpdate());
            }
        }

        public void OnTransitionExit()
        {
            calculator.OnTransitionExitEvent -= ActionStop;
            ActionStop();
        }
        #endregion               

        #region ===================================Animator control
        /// <summary>
        /// Only allow in Boolean or Custom with isTransitionTrigger on option in ActionType.
        /// </summary>
        /// <param name="value"></param>     
        protected void SetAnimatorBool(bool value)
        {
            if (isAttachWithAnimator)
            {
                if (isTransitionTrigger)
                {
                    Debug.LogWarning("SetAnimationBool function shouldn't be called by " +
                        this +
                        " which set isTransitionTrigger true"
                    );
                    return;
                }
                animator.SetBool("Is" + GetName(), value);
            }
        }
        private void SetAnimatorTrigger()
        {
            if (isAttachWithAnimator)
            {
                Assert.IsTrue(isTransitionTrigger);
                animator.SetTrigger(GetName());
            }
        }

        private string GetName()
        {
            //return GetType().Name.Replace("Action", "");
            return GetType().Name;
        }
        #endregion
    }
}