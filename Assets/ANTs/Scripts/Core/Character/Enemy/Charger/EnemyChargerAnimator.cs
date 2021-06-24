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
            GetComponent<Mover>().OnStartMovingEvent += OnStopMoving;
        }

        private void OnDisable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent -= OnActorAtack;
            GetComponent<Mover>().OnStartMovingEvent -= OnStartMoving;
            GetComponent<Mover>().OnStartMovingEvent -= OnStopMoving;
        }

        private void OnActorAtack()
        {
            animator.SetTrigger(Triggers.StartAttacking);
        }

        private void OnStartMoving()
        {
            animator.SetTrigger(Triggers.StartMoving);
        }

        private void OnStopMoving()
        {
            animator.SetTrigger(Triggers.StopMoving);
        }
    }
}