using ANTs.Template;
using Panda;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyNinjaControl))]
    public class EnemyNinjaTasks : MonoBehaviour
    {
        public Transform player;
        [SerializeField] ANTsPolygon prepareZone;

        private EnemyNinjaControl enemy;
        private bool isArrived;
        private bool isAttackDone;
        private Vector2 preparePosition;

        #region ======================================================Initialize
        public void Awake()
        {
            enemy = GetComponent<EnemyNinjaControl>();
            preparePosition = prepareZone.GetRandomPointOnSurface();
        }

        public void OnEnable()
        {
            enemy.OnActorArrivedEvent += OnActorArrived;
            enemy.OnActorAttackDone += OnActorAttackDone;
        }

        private void OnDisable()
        {
            enemy.OnActorArrivedEvent -= OnActorArrived;
            enemy.OnActorAttackDone -= OnActorAttackDone;
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
                enemy.Attack();
            }

            if (isAttackDone) Task.current.Succeed();
        }
        #endregion





        public void OnActorArrived()
        {
            isArrived = true;
        }

        private void OnActorAttackDone()
        {
            isAttackDone = true;
        }
    }
}