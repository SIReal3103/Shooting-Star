using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyFacade))]
    public class EnemyChargerBehaviour : MonoBehaviour
    {
        public event Action OnArrivedEvent
        {
            add { control.OnArrivedEvent += value; }
            remove { control.OnArrivedEvent -= value; }
        }

        [SerializeField] MoveData moveSpeed;
        [SerializeField] MoveData runSpeed;

        private EnemyFacade control;

        private void Awake()
        {
            control = GetComponent<EnemyFacade>();
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