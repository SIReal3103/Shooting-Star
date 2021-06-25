using UnityEngine;
using ANTs.Template;
using System;

namespace ANTs.Core
{
    [RequireComponent(typeof(ChargeToPlayerTask))]
    [RequireComponent(typeof(MoverAction))]
    public class EnemyChargerAnimator : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private MoverAction mover;
        private EnemyFacade enemy;

        private void Awake()
        {
            mover = GetComponent<MoverAction>();
            enemy = GetComponent<EnemyFacade>();
        }

        private void OnEnable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent += OnActorAtack;
            GetComponent<DieAction>().OnActionStart += OnEnemyDead;
        }

        private void OnDisable()
        {
            GetComponent<ChargeToPlayerTask>().OnActorAttackEvent -= OnActorAtack;
            GetComponent<DieAction>().OnActionStart -= OnEnemyDead;
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