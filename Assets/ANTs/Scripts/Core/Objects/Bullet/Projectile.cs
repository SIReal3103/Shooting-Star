using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Mover))]
    public abstract class Projectile : MonoBehaviour
    {
        public GameObject source;

        private Mover mover;
        protected virtual void Awake()
        {
            mover = GetComponent<Mover>();
            LoadMoverStrategy();
        }

        private void LoadMoverStrategy()
        {
            mover.SetMoveStrategy(MoveFactory.CreateMove(MovementType.Linearity));
        }

        public void SetDirection(Vector2 direction)
        {
            mover.StartMovingTo(direction * 1000);
        }
    }
}