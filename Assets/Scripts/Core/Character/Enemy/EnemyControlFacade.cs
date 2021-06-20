using UnityEngine;

using ANTs.Template;

namespace ANTs.Game
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Mover))]
    public class EnemyControlFacade : MonoBehaviour, IANTsPoolObject<EnemyPool, EnemyControlFacade>
    {
        private Mover mover;

        public EnemyPool CurrentPool { get; set; }

        private void Start()
        {
            mover = GetComponent<Mover>();
            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Linearity);
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

