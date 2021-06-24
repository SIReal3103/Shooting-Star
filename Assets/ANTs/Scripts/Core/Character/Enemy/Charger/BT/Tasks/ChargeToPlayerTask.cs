using UnityEngine;
using Panda;

namespace ANTs.Core
{
    public class ChargeToPlayerTask : MonoBehaviour
    {
        public event System.Action OnActorAttackEvent;

        [SerializeField] Transform player;

        private EnemyChargerBehaviour charger;

        private bool isArrive;

        private void Awake()
        {
            charger = GetComponent<EnemyChargerBehaviour>();
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
            charger.ChargeTo(playerPosition);
        }

        public void OnMoverArrived()
        {
            isArrive = true;
        }
    }
}