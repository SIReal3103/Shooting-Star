using UnityEngine;

namespace ANTs.Game
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
            GetComponent<Damageable>().OnActorDieEvent += GetComponent<DieAction>().ActionStart;
        }

        private void OnDisable()
        {
            GetComponent<Damageable>().OnActorDieEvent -= GetComponent<DieAction>().ActionStart;
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