using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MoverAction))]
    [RequireComponent(typeof(GunnerAction))]
    public class PlayerFacade : MonoBehaviour
    {
        private MoverAction mover;
        private GunnerAction gunner;

        private void Awake()
        {
            mover = GetComponent<MoverAction>();
            gunner = GetComponent<GunnerAction>();
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