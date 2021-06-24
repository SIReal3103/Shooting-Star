using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Gunner))]
    public class PlayerFacadel : MonoBehaviour
    {
        private Mover mover;
        private Gunner gunner;

        private void Start()
        {
            mover = GetComponent<Mover>();
            gunner = GetComponent<Gunner>();
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