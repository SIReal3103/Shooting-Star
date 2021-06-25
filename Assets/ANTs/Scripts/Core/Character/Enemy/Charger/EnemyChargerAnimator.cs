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

        private Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void OnEnable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent += OnActorAtack;
        }

        private void OnDisable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent -= OnActorAtack;
        }

        private void Update()
        {
            animator.SetBool(ANTsTransition.IsMoving, mover.IsMoving());
        }

        private void OnActorAtack()
        {
            animator.SetTrigger(ANTsTransition.StartAttacking);
        }
    }
}