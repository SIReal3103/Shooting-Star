using ANTs.Core;
using ANTs.Template;
using Panda;
using UnityEngine;

namespace Assets.ANTs.Scripts.Core.Character.Enemy.Charger.BT.Tasks
{
    public class AttackTask : MonoBehaviour
    {
        [SerializeField] AttackArea attackArea;
        [SerializeField] ANTsAnimationEvents animationEvent;

        private bool isAttackDone;

        private void OnEnable()
        {
            animationEvent.OnActorAttackEvent += OnActorAttack;
        }

        private void OnDisable()
        {
            animationEvent.OnActorAttackEvent -= OnActorAttack;
        }

        [Task]
        public void AttackPlayer()
        {
            if (Task.current.isStarting)
            {
                isAttackDone = false;
            }

            if (isAttackDone == true)
            {
                attackArea.Attack();
                Task.current.Succeed();
            }
        }

        private void OnActorAttack()
        {
            isAttackDone = true;
        }
    }
}