using UnityEngine;
using Panda;

namespace ANTs.Core
{
    public class ChargeToPlayerTask : MonoBehaviour
    {
        [SerializeField] Transform player;

        private EnemyChargeBehaviour charger;

        private bool isArrive;

        private void Awake()
        {
            charger = GetComponent<EnemyChargeBehaviour>();
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