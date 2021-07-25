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
        private bool isAttackDone;

        private void Awake()
        {
            move = GetComponent<MoveAction>();
            attack = GetComponent<MeleeAttackAction>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnEnable()
        {
            attack.OnActionStopEvent += OnActorAttackFinished;
        }

        private void OnDisable()
        {
            attack.OnActionStopEvent -= OnActorAttackFinished;
        }

        #region ==================================================Tasks        
        [Task]
        public void ApproachPlayer()
        {
            if (Task.current.isStarting)
            {
                move.SetMoveData(runSpeed);
                move.StartMovingTo(player.position);
            }
            
            if(move.IsArrived())
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
                move.StartMovingTo(prepareZone.GetRandomPointOnSurface(), Task.current.Succeed);
            }

            if(move.IsArrived())
            {
                Task.current.Succeed();
            }
        }

        [Task]
        public void AttackPlayer()
        {
            if (Task.current.isStarting)
            {
                isAttackDone = false;
                attack.ActionStart();
            }
            if (isAttackDone) Task.current.Succeed();            
        }
        #endregion

        void Succeed(Task task)
        {
            task.Succeed();
        }

        private void OnActorAttackFinished()
        {
            isAttackDone = true;
        }
    }
}