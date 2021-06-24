using UnityEngine;
using ANTs.Template;
using System;

namespace ANTs.Core
{
    [RequireComponent(typeof(ChargeToPlayerTask))]
    [RequireComponent(typeof(Mover))]
    public class EnemyChargerAnimator : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private void OnEnable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent += OnActorAtack;
            GetComponent<Mover>().OnStartMovingEvent += OnStartMoving;
            GetComponent<Mover>().OnStopMovingEvent += OnStopMoving;
        }

        private void OnDisable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent -= OnActorAtack;
            GetComponent<Mover>().OnStartMovingEvent -= OnStartMoving;
            GetComponent<Mover>().OnStopMovingEvent -= OnStopMoving;
        }

        private void OnActorAtack()
        {
            animator.SetTrigger(ANTsTransition.StartAttacking);
        }

        private void OnStartMoving()
        {
            animator.SetBool(ANTsTransition.IsMoving, true);
        }

        private void OnStopMoving()
        {
            animator.SetBool(ANTsTransition.IsMoving, false);
        }
    }
}