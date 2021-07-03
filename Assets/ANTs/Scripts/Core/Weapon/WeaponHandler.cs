using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [SerializeField] bool rotateWithMouse;
        [Space(20)]
        [SerializeField] Weapon initialWeapon;
        [SerializeField] WeaponAmmo initialAmmo;

        public ProjectileWeapon currentProjectileWeapon;
        public MeleeWeapon currentMeleeWeapon;
        private Weapon currentWeapon;
        private ANTsPool currentAmmo;

        private void Start()
        {
            currentAmmo = initialAmmo ? initialAmmo.gameObject.GetOrCreatePool() : AmmoManager.Instance.GetDefaultPool();

            if (initialWeapon)
            {
                currentWeapon = initialWeapon;
                if (currentWeapon is ProjectileWeapon)
                {
                    InitProjectileWeapon(currentAmmo);
                }
                else if (currentWeapon is MeleeWeapon)
                {
                    InitMeleeWeapon();
                }
                else
                {
                    throw new UnityException("Invalid type of initialWeapon");
                }
            }
        }

        public void TriggerWeapon()
        {
            currentWeapon.TriggerWeapon();
        }

        public void SetDirection(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            weaponAttachment.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ - 90));
        }

        public void UpgradeCurrentWeapon()
        {
            currentWeapon = WeaponUpgradeHandler.GetUpgradedWeapon(currentWeapon);
        }

        private void InitMeleeWeapon()
        {
            currentMeleeWeapon = currentWeapon as MeleeWeapon;
            currentMeleeWeapon.SetOwner(gameObject);
        }

        private void InitProjectileWeapon(ANTsPool ammoPool)
        {
            currentProjectileWeapon = currentWeapon as ProjectileWeapon;
            currentProjectileWeapon.SetAmmoPool(ammoPool);
            currentProjectileWeapon.SetOwner(gameObject);
        }
    }
}