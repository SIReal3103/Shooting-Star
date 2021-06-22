using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Mover))]
    public abstract class Projectile : MonoBehaviour
    {
        public GameObject source;

        private Mover mover;

        private void OnEnable()
        {
            LoadMoverStrategy();
        }

        private void LoadMoverStrategy()
        {
            if (mover == null)
            {
                mover = GetComponent<Mover>();
                mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Linearity);
            }
        }

        public void SetDirection(Vector2 direction)
        {
            mover.StartMovingTo(direction * 1000);
        }
    }
}