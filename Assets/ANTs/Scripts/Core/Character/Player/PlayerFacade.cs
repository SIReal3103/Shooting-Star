using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MoveAction))]
    [RequireComponent(typeof(ShootAction))]
    public class PlayerFacade : MonoBehaviour
    {
        private MoveAction mover;
        private ShootAction gunner;

        private void Awake()
        {
            mover = GetComponent<MoveAction>();
            gunner = GetComponent<ShootAction>();
        }

        public void StartMovingTo(Vector2 position)
        {
            mover.SetDestination(position);
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