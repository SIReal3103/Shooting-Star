using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(EnemyFacade))]
    public class EnemyChargerBehaviour : MonoBehaviour
    {
        public event Action OnArrivedEvent
        {
            add { GetComponent<EnemyFacade>().OnArrivedEvent += value; }
            remove { GetComponent<EnemyFacade>().OnArrivedEvent -= value; }
        }

        [SerializeField] MoveData moveSpeed;
        [SerializeField] MoveData runSpeed;

        private EnemyFacade control;

        private void Awake()
        {
            control = GetComponent<EnemyFacade>();
            GetComponent<TouchDamager>().source = gameObject;
        }

        public void MoveTo(Vector2 destination)
        {
        }

        public void ChargeTo(Vector2 destination)
        {
        }
    }
}