using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Gunner))]
    public class PlayerControlFacade : MonoBehaviour
    {
        private Mover mover;
        private Gunner gunner;

        private void Start()
        {
            mover = GetComponent<Mover>();
            gunner = GetComponent<Gunner>();

            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Lerp);
        }

        public void StartMovingTo(Vector2 position)
        {
            mover.StartMovingTo(position);
        }

        public void ChangeStrongerGun()
        {
            gunner.ChangeStrongerGun();
        }

        public void ChangeStrongerBullet()
        {
            gunner.ChangeStrongerBullet();
        }
    }
}