using System;
using UnityEngine;

namespace ANTs.Template
{
    [RequireComponent(typeof(Animator))]
    public class ANTsAnimationEvents : MonoBehaviour
    {
        public event Action OnActorAttackEvent;
        public void OnActorAttack()
        {
            OnActorAttackEvent?.Invoke();
        }
    }
}