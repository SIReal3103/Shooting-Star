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
            OnActionStart?.Invoke();
            isActionStart = true;
            GetComponent<ActionScheduler>().Trigger(this);
        }
        public virtual void ActionStop()
        {
            // Debug.Log(GetType().Name + " Action Stop!");
            OnActionStop?.Invoke();
            isActionStart = false;
        }
    }
}