using UnityEngine;

namespace ANTs.Template
{
    public abstract class ActionBase : MonoBehaviour
    {
        public event System.Action OnActionStart;
        public event System.Action OnActionStop;

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
        }
        public virtual void ActionStop()
        {
            OnActionStop?.Invoke();
            actionStartOnPlay = false;
        }
    }
}