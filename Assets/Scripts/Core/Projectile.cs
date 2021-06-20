using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(Mover))]
    public abstract class Projectile : MonoBehaviour
    {
        private Mover mover;

        public GameObject Source { get; set; }

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