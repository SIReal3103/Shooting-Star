using System;
using UnityEngine;

namespace ANTs.Template
{
    [RequireComponent(typeof(ActionScheduler))]
    public abstract class ActionBase : MonoBehaviour
    {
        public event Action OnActionStart;
        public event Action OnActionStop;

        [Header("ActionBase")]
        [SerializeField] bool actionStartOnPlay = false;
        [ReadOnly]
        [SerializeField] bool isActionStart = false;

        [HideInInspector]
        public Animator animator;

        protected virtual void Start()
        {
            if (actionStartOnPlay)
                ActionStart();
        }

        protected bool IsActionStart { get => isActionStart; }

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
            isActionStart = true;

            OnActionStart?.Invoke();

            if (animator) animator.SetTrigger(GetType().Name + "Start");

            GetComponent<ActionScheduler>().StopActionRelavetiveTo(this);
        }

        public virtual void ActionStop()
        {
            isActionStart = false;

            if (animator) animator.SetTrigger(GetType().Name + "Start");

            OnActionStop?.Invoke();
        }
    }
}