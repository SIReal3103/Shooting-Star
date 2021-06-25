using UnityEngine;
using Panda;
using ANTs.Template;
using ANTs.Core;

namespace Assets.ANTs.Scripts.Core.Character.Enemy.Charger.BT.Tasks
{
    public class AttackTask : MonoBehaviour
    {
        [SerializeField] AttackArea attackArea;
        [SerializeField] ANTsAnimationEvents events;

        private bool isAttackDone;

        private void OnEnable()
        {
            events.OnActorAttackEvent += OnActorAttack;
        }

        private void OnDisable()
        {
            events.OnActorAttackEvent -= OnActorAttack;
        }

        [Task]
        public void AttackPlayer()
        {
            if(Task.current.isStarting)
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