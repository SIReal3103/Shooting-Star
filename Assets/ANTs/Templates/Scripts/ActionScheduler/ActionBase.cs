using System;
using UnityEngine;

namespace ANTs.Template
{
    [RequireComponent(typeof(ActionScheduler))]
    public abstract class ActionBase : MonoBehaviour, IAction
    {
        public event Action OnActionStartEvent;
        public event Action OnActionStopEvent;

        [Header("ActionBase")]
        [SerializeField] bool actionStartOnPlay = false;
        [ReadOnly]
        [SerializeField] bool isActionStart = false;

        [HideInInspector]
        protected Animator animator;
        private ActionScheduler scheduler;

        public bool IsActionStart { get => isActionStart; }

        protected virtual void Awake()
        {
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
            if (IsActionStart == true)
            {
                ActionUpdate();
            }
        }

        protected abstract void ActionUpdate();

        public virtual void ActionStart()
        {
            if (scheduler.IsActionPrevent(this)) return;

            isActionStart = true;
            OnActionStartEvent?.Invoke();
            SetTriggerStart();

            scheduler.StopActionRelavetiveTo(this);
        }

        public virtual void ActionStop()
        {
            isActionStart = false;
            OnActionStopEvent?.Invoke();
            SetTriggerStop();
        }

        protected void SetAnimationBool(bool value)
        {
            if (animator) animator.SetBool("Is" + GetType().Name, value);
        }

        private void SetTriggerStart()
        {
            if (animator) animator.SetTrigger(GetType().Name + "Start");
        }

        private void SetTriggerStop()
        {
            if (animator) animator.SetTrigger(GetType().Name + "Stop");
        }
    }
}