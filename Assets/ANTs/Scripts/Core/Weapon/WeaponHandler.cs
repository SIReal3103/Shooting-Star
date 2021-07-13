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

        [HideInInspector]
        public ProjectileWeapon currentProjectileWeapon;
        [HideInInspector]
        public MeleeWeapon currentMeleeWeapon;

        private Weapon currentWeapon;
        private ANTsPool currentAmmo;

        private void Awake()
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
            else
            {
                throw new UnityException("No initial weapon avaiable");
            }
        }

        #region ==================================== TRIGGERS
        public void TriggerCurrentWeapon()
        {
            currentWeapon.TriggerWeapon();
        }

        public void TriggerMeleeWeapon()
        {
            currentMeleeWeapon.TriggerWeapon();
        }

        public void TriggerProjectileWeapon()
        {
            currentProjectileWeapon.TriggerWeapon();
        }
        #endregion

        public void SetDirectionLookAt(Vector2 position)
        {
            Vector2 delt = position - (Vector2)transform.position;
            Debug.Log(position);
            float ang = Mathf.Atan2(delt.y, delt.x) * Mathf.Rad2Deg;
            weaponAttachment.eulerAngles = new Vector3(0, 0, ang - 90f);
        }

        public void UpgradeCurrentWeapon()
        {
            WeaponUpgradeHandler.UpgradeWeapon(ref currentWeapon);
        }

        public void UpgradeCurrentAmmo()
        {
            WeaponUpgradeHandler.UpgradeWeaponAmmo(currentProjectileWeapon, ref currentAmmo);
        }

        private void InitMeleeWeapon()
        {
            currentMeleeWeapon = (MeleeWeapon)currentWeapon;
            currentMeleeWeapon.Init(new MeleeWeaponData(gameObject, transform));
        }

        private void InitProjectileWeapon(ANTsPool ammoPool)
        {
            currentProjectileWeapon = currentWeapon as ProjectileWeapon;
            currentProjectileWeapon.Init(new ProjectileWeaponData(gameObject, transform, ammoPool));
        }
    }
}