using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyFacade))]
    public class EnemyNinjaFacade : MonoBehaviour
    {
        public event Action OnArrivedEvent
        {
            add { GetComponent<MoverAction>().OnArrivedEvent += value; }
            remove { GetComponent<MoverAction>().OnArrivedEvent -= value; }
        }

        [Tooltip("The speed which Charger normally move")]
        [SerializeField] MoveData normalSpeed;
        [Tooltip("The speed which charger run to target (or player)")]
        [SerializeField] MoveData chargeSpeed;

        private MoverAction mover;

        private void Awake()
        {
            mover = GetComponent<MoverAction>();
        }

        public void MoveTo(Vector2 destination)
        {
            mover.SetMoveData(normalSpeed);
            mover.SetDestination(destination);
        }

        public void RunTo(Vector2 destination)
        {
            mover.SetMoveData(chargeSpeed);
            mover.SetDestination(destination);
        }
    }
}