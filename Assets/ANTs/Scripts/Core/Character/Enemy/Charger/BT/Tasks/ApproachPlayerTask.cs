using Panda;
using UnityEngine;

namespace ANTs.Core
{
    public class ApproachPlayerTask : MonoBehaviour
    {
        public event System.Action OnActorAttackEvent;

        [SerializeField] Transform player;

        private EnemyNinjaFacade charger;

        private bool isArrive;

        private void Awake()
        {
            charger = GetComponent<EnemyNinjaFacade>();
        }

        private void OnEnable()
        {
            charger.OnArrivedEvent += OnMoverArrived;
        }

        private void OnDisable()
        {
            charger.OnArrivedEvent -= OnMoverArrived;
        }

        [Task]
        public void ChargeToPlayer()
        {
            if (Task.current.isStarting)
            {
                Revaluate();
            }

            if (isArrive)
            {
                Task.current.Succeed();
                OnActorAttackEvent?.Invoke();
            }
            Task.current.debugInfo = Task.current.status.ToString();
        }

        private void Revaluate()
        {
            isArrive = false;
            Vector2 playerPosition = player.position;
            charger.RunTo(playerPosition);
        }

        public void OnMoverArrived()
        {
            isArrive = true;
        }
    }
}