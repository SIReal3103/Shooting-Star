using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MoveAction))]
    [RequireComponent(typeof(ShootAction))]
    [RequireComponent(typeof(Damageable))]
    public class PlayerControl : MonoBehaviour
    {
        private MoveAction mover;

        private void Awake()
        {
            mover = GetComponent<MoveAction>();
        }

        private void OnEnable()
        {
            GetComponent<Damageable>().OnHealthReachZeroEvent += GetComponent<DieAction>().ActionStart;
        }

        private void OnDisable()
        {
            GetComponent<Damageable>().OnHealthReachZeroEvent += GetComponent<DieAction>().ActionStop;
        }        

        public void StartMovingTo(Vector2 position)
        {
            mover.ActionStart();
            mover.SetDestination(position);
        }

        public void UpgradeWeapon()
        {
            GetComponent<WeaponHandler>().UpgradeProjectileWeapon();
        }

        public void ChangeStrongerBullet()
        {
            GetComponent<WeaponHandler>().UpgradeCurrentAmmo();
        }
    }
}