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
        public Animator animator;

        public bool IsActionStart
        {
            get => isActionStart;
            private set
            {
                if (isActionStart == value) return;
                if (value) OnActionStartEvent?.Invoke();
                else OnActionStopEvent?.Invoke();
                isActionStart = value;
            }
        }

        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
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
            IsActionStart = true;
            if (animator) animator.SetTrigger(GetType().Name + "Start");
            GetComponent<ActionScheduler>().StopActionRelavetiveTo(this);
        }

        public virtual void ActionStop()
        {
            IsActionStart = false;
            if (animator) animator.SetTrigger(GetType().Name + "Stop");
        }
    }
}