using System;
using UnityEngine;

using ANTs.Template;

namespace ANTs.Game
{
    [RequireComponent(typeof(Mover))]
    public class EnemyControlFacade : MonoBehaviour, IANTsPoolObject<EnemyPool, EnemyControlFacade>
    {
        public event Action onMoverArrivedEvent;

        private Mover mover;

        public EnemyPool CurrentPool { get; set; }

        //TODO: Enemy not die

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void OnEnable()
        {
            mover.onArrivedEvent += OnMoverArrive;
        }

        private void OnDisable()
        {
            mover.onArrivedEvent -= OnMoverArrive;
        }

        private void Start()
        {
            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Linearity);
        }

        private void OnMoverArrive()
        {
            onMoverArrivedEvent?.Invoke();
        }

        public void StartMovingTo(Vector2 destination)
        {
            mover.StartMovingTo(destination);
        }

        public void StopMoving()
        {
            mover.StopMoving();
        }

        public void Dead()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            CurrentPool.ReturnToPool(this);
        }

        // IANTsPoolObject implementation

        public void WakeUp(object args)
        {
            EnemyData data = args as EnemyData;
            gameObject.SetActive(true);
            transform.position = data.spawnPosition;
        }

        public void Sleep()
        {
            gameObject.SetActive(false);
        }
    }
}

