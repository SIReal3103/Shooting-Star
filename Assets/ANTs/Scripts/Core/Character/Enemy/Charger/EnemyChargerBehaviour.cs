using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyFacde))]
    public class EnemyChargerBehaviour : MonoBehaviour
    {
        public event Action OnArrivedEvent
        {
            add { GetComponent<EnemyFacde>().OnArrivedEvent += value; }
            remove { GetComponent<EnemyFacde>().OnArrivedEvent -= value; }
        }

        [SerializeField] MoveData moveSpeed;
        [SerializeField] MoveData runSpeed;

        private EnemyFacde control;

        private void Awake()
        {
            control = GetComponent<EnemyFacde>();
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