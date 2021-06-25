using System;
using UnityEngine;

namespace ANTs.Template
{
    public class ANTsAnimationEvents : MonoBehaviour
    {
        public event Action OnActorAttackEvent;
        public void OnActorAttack() {
            OnActorAttackEvent?.Invoke();
        }
    }
}