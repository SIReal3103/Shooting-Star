using UnityEngine;

namespace ANTs.Template
{
    public abstract class ActionBase : MonoBehaviour
    {
        protected bool isActionStart = true;

        protected void Update()
        {
            if(isActionStart == true)
            {
                ActionUpdate();
            }
        }

        public abstract void ActionUpdate();

        public virtual void ActionStart()
        {
            isActionStart = true;
        }
        public virtual void ActionStop()
        {
            isActionStart = false;
        }
    }
}