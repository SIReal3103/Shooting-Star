using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyBasicBehaviour))]
    public class EnemyChargerBehaviour : MonoBehaviour
    {
        public event Action OnArrivedEvent
        {
            add { GetComponent<EnemyBasicBehaviour>().OnArrivedEvent += value; }
            remove { GetComponent<EnemyBasicBehaviour>().OnArrivedEvent -= value; }
        }

        [SerializeField] MoveData moveSpeed;
        [SerializeField] MoveData runSpeed;

        private EnemyBasicBehaviour control;

        private void Awake()
        {
            control = GetComponent<EnemyBasicBehaviour>();
            GetComponent<TouchDamager>().source = gameObject;
        }

        public void MoveTo(Vector2 destination)
        {
            control.LoadMoveData(moveSpeed);
            control.StartMovingTo(destination);
        }
        public void ChargeTo(Vector2 destination)
        {
            control.LoadMoveData(runSpeed);
            control.StartMovingTo(destination);
        }
    }
}