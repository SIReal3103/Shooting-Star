using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Gunner))]
    public class PlayerFacade : MonoBehaviour
    {
        private Mover mover;
        private Gunner gunner;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            gunner = GetComponent<Gunner>();
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