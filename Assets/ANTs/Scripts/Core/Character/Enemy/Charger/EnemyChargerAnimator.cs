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
        private EnemyFacade enemy;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            enemy = GetComponent<EnemyFacade>();
        }

        private void OnEnable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent += OnActorAtack;
            GetComponent<Dead>().OnActionStart += OnEnemyDead;
        }

        private void OnDisable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent -= OnActorAtack;
            GetComponent<Dead>().OnActionStart -= OnEnemyDead;
        }

        private void Update()
        {
            animator.SetBool(ANTsGameState.IsMoving, mover.IsMoving());
        }

        private void OnActorAtack()
        {
            animator.SetTrigger(ANTsGameState.StartAttacking);
        }

        private void OnEnemyDead()
        {
            animator.SetTrigger(ANTsGameState.StartDying);
        }
    }
}