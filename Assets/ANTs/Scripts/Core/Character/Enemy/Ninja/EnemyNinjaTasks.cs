using ANTs.Template;
using UnityEngine;
using Panda;

namespace ANTs.Core
{
    public class EnemyNinjaTasks : MonoBehaviour
    {
        public event System.Action OnActorAttackEvent;

        [SerializeField] Transform player;
        [SerializeField] ANTsPolygon prepareZone;
        [SerializeField] AnimatorEvents animationEvent;

        private EnemyNinjaFacade enemy;
        private bool isArrived;
        private bool isAttackDone;
        private Vector2 preparePosition;

        #region ======================================================Initialize
        public void Awake()
        {
            enemy = GetComponent<EnemyNinjaFacade>();
        }

        public void OnEnable()
        {
            enemy.OnArrivedEvent += OnMoverArrived;
            animationEvent.OnActorAttackEvent += OnActorAttack;
        }

        private void OnDisable()
        {
            enemy.OnArrivedEvent -= OnMoverArrived;
            animationEvent.OnActorAttackEvent -= OnActorAttack;
        }
        #endregion




        #region ==================================================Tasks
        [Task]
        public void ApproachPlayer()
        {
            if (Task.current.isStarting)
            {
                isArrived = false;
                Vector2 playerPosition = player.position;
                enemy.RunTo(playerPosition);
            }

            if (isArrived)
            {
                Task.current.Succeed();
                OnActorAttackEvent?.Invoke();
            }
            Task.current.debugInfo = Task.current.status.ToString();
        }

        [Task]
        public void MoveToPreparePosition()
        {
            if (Task.current.isStarting)
            {
                isArrived = false;
                enemy.MoveTo(preparePosition);
            }

            if (isArrived) Task.current.Succeed();
        }

        [Task]
        public void AttackPlayer()
        {
            if (Task.current.isStarting)
            {
                isAttackDone = false;
            }

            if (isAttackDone) Task.current.Succeed();
        }

        public void OnMoverArrived()
        {
            isArrived = true;
        }

        private void OnActorAttack()
        {
            isAttackDone = true;
        }
        #endregion
    }
}