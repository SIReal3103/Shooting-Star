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

        protected bool IsActionStart { get => actionStartOnPlay; }

        protected void Update()
        {
            if(IsActionStart == true)
            {
                ActionUpdate();
            }
        }

        protected abstract void ActionUpdate();

        public virtual void ActionStart()
        {
            OnActionStart?.Invoke();
            actionStartOnPlay = true;
            GetComponent<ActionScheduler>().Trigger(this);
        }
        public virtual void ActionStop()
        {
            OnActionStop?.Invoke();
            actionStartOnPlay = false;
        }
    }
}