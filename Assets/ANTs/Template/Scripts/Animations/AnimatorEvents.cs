using System;
using UnityEngine;

namespace ANTs.Template
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorEvents : MonoBehaviour
    {
        public event Action OnActorAttackEvent;
        private void OnActorAttack()
        {
            OnActorAttackEvent?.Invoke();
        }
    }
}