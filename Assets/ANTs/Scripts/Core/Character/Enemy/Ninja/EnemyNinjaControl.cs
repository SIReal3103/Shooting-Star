using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyControl))]
    public class EnemyNinjaControl : MonoBehaviour
    {
        public event Action OnActorArrivedEvent
        {
            add { GetComponent<MoveAction>().OnArrivedEvent += value; }
            remove { GetComponent<MoveAction>().OnArrivedEvent -= value; }
        }

        public event Action OnActorAttackDone
        {
            add { GetComponent<AttackAction>().OnActionStopEvent += value; }
            remove { GetComponent<AttackAction>().OnActionStopEvent -= value; }
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
            attack = GetComponent<AttackAction>();
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
            attack.ActionStart();
        }
    }
}