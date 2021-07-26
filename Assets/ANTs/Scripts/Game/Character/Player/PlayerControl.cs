using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(MoveAction))]
    [RequireComponent(typeof(WeaponHandler))]
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

        public void StartMovingWith(Vector2 direction)
        {
            mover.StartMovingWith(direction);
        }

        public void StartMovingTo(Vector2 destination)
        {
            mover.StartMovingTo(destination);
        }

        public void DirectWeaponTo(Vector2 position)
        {
            GetComponent<WeaponHandler>().DirectWeaponAttachmentTo(position);
        }

        public void FacingTo(Vector2 position)
        {
            mover.FacingTo(position);
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