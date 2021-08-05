using ANTs.Template;
using Panda;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(EnemyControl))]
    [RequireComponent(typeof(MoveAction))]
    [RequireComponent(typeof(MeleeAttackAction))]
    public class EnemyNinjaControl : MonoBehaviour
    {
        [SerializeField] MoveData normalSpeed;
        [SerializeField] MoveData runSpeed;
        [SerializeField] ANTsPolygon prepareZone;

        private MoveAction move;
        private MeleeAttackAction attack;
        private Transform player;

        private void Awake()
        {
            move = GetComponent<MoveAction>();
            attack = GetComponent<MeleeAttackAction>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        [Task]
        public void ApproachPlayer()
        {
            if (Task.current.isStarting)
            {
                move.SetMoveData(runSpeed);
                move.StartMovingTo(player.position);
            }

            if (move.IsArrived())
            {
                Task.current.Succeed();
            }
        }

        [Task]
        public void MoveToPreparePosition()
        {
            if (Task.current.isStarting)
            {
                move.SetMoveData(normalSpeed);
                move.StartMovingTo(prepareZone.GetRandomPointOnSurface());
            }

            if (move.IsArrived())
            {
                Task.current.Succeed();
            }
        }

        [Task]
        public void AttackPlayer()
        {
            if (Task.current.isStarting)
            {
                attack.ActionStart();
            }
            if (!attack.IsActionActive) Task.current.Succeed();
        }
    }
}