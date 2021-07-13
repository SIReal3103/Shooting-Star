using ANTs.Template;
using Panda;
using UnityEngine;

namespace ANTs.Core
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
        private bool isArrived;
        private bool isAttackDone;

        private void Awake()
        {
            move = GetComponent<MoveAction>();
            attack = GetComponent<MeleeAttackAction>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            move.OnArrivedEvent += OnActorArrived;
            attack.OnActionStopEvent += OnActorAttackFinished;
        }

        #region ==================================================Tasks        
        [Task]
        public void ApproachPlayer()
        {
            if (Task.current.isStarting)
            {
                isArrived = false;
                move.SetMoveData(runSpeed);
                move.SetDestination(player.position);
            }

            if (isArrived)
            {
                Task.current.Succeed();
            }
            Task.current.debugInfo = Task.current.status.ToString();
        }

        [Task]
        public void MoveToPreparePosition()
        {
            if (Task.current.isStarting)
            {
                isArrived = false;
                move.SetMoveData(normalSpeed);
                move.SetDestination(prepareZone.GetRandomPointOnSurface());
            }

            if (isArrived) Task.current.Succeed();
        }

        [Task]
        public void AttackPlayer()
        {
            if (Task.current.isStarting)
            {
                attack.ActionStart();
            }

            if (isAttackDone) Task.current.Succeed();
        }
        #endregion

        public void OnActorArrived()
        {
            isArrived = true;
        }

        private void OnActorAttackFinished()
        {
            isAttackDone = true;
        }
    }
}