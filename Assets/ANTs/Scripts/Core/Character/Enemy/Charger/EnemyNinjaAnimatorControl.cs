using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(ApproachPlayerTask))]
    [RequireComponent(typeof(MoverAction))]
    public class EnemyNinjaAnimatorControl : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private MoverAction mover;

        private void Awake()
        {
            mover = GetComponent<MoverAction>();
        }

        private void OnEnable()
        {
            GetComponent<ApproachPlayerTask>().OnActorAttackEvent += OnActorAtack;
            GetComponent<DieAction>().OnActionStart += OnEnemyDead;
        }

        private void OnDisable()
        {
            GetComponent<ApproachPlayerTask>().OnActorAttackEvent -= OnActorAtack;
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