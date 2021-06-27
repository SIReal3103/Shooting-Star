using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyFacade))]
    public class EnemyNinjaFacade : MonoBehaviour
    {
        public event Action OnArrivedEvent
        {
            add { GetComponent<MoveAction>().OnArrivedEvent += value; }
            remove { GetComponent<MoveAction>().OnArrivedEvent -= value; }
        }

        [Tooltip("The speed which Charger normally move")]
        [SerializeField] MoveData normalSpeed;
        [Tooltip("The speed which charger run to target (or player)")]
        [SerializeField] MoveData chargeSpeed;

        private MoveAction move;
        private AttackAction attack;

        private void Awake()
        {
            move = GetComponent<MoveAction>();
        }

        public void MoveTo(Vector2 destination)
        {
            move.SetMoveData(normalSpeed);
            move.SetDestination(destination);
        }

        public void RunTo(Vector2 destination)
        {
            move.SetMoveData(chargeSpeed);
            move.SetDestination(destination);
        }

        public void Attack()
        {
            attack.Attack();
        }
    }
}