using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyFacade))]
    public class EnemyChargerBehaviour : MonoBehaviour
    {
        public event Action OnArrivedEvent
        {
            add { GetComponent<Mover>().OnArrivedEvent += value; }
            remove { GetComponent<Mover>().OnArrivedEvent -= value; }
        }

        [Tooltip("The speed which Charger normally move")]
        [SerializeField] MoveData normalSpeed;
        [Tooltip("The speed which charger run to target (or player)")]
        [SerializeField] MoveData chargeSpeed;

        private Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        public void MoveTo(Vector2 destination)
        {
            mover.SetMoveData(normalSpeed);
            mover.StartMovingTo(destination);
        }

        public void ChargeTo(Vector2 destination)
        {
            mover.SetMoveData(chargeSpeed);
            mover.StartMovingTo(destination);
        }
    }
}