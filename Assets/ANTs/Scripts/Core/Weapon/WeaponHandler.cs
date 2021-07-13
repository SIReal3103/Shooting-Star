using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [SerializeField] bool rotateWithMouse;
        [Space(20)]
        [SerializeField] bool initialWeaponIsMelee = true;
        [SerializeField] string initialWeaponName;
        [Conditional("initialWeaponIsMelee", false)]
        [SerializeField] string initialAmmoName;

        [ReadOnly]
        public Weapon currentWeapon;

        private ProjectileWeapon currentProjectileWeapon;
        private MeleeWeapon currentMeleeWeapon;
        private ANTsPool currentAmmoPool;


        private void Start()
        {
            if(initialWeaponIsMelee)
            {
                InitMeleeWeapon();
                currentWeapon = currentMeleeWeapon;
            }
            else
            {
                InitAmmo();
                InitProjectileWeapon(currentAmmoPool);
                currentWeapon = currentProjectileWeapon;
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
            WeaponUpgradeHandler.UpgradeWeaponAmmo(currentProjectileWeapon, ref currentAmmoPool);
        }

        private void InitMeleeWeapon()
        {
            if (!MeleeWeaponManager.Instance.TryGetPool(initialWeaponName, out ANTsPool weaponPool))
            {
                weaponPool = MeleeWeaponManager.Instance.GetDefaultPool();
            }
            currentMeleeWeapon = weaponPool.Pop(new MeleeWeaponData(gameObject, weaponAttachment)).GetComponent<MeleeWeapon>();
        }

        private void InitProjectileWeapon(ANTsPool ammoPool)
        {
            if (!ProjectileWeaponManager.Instance.TryGetPool(initialWeaponName, out ANTsPool weaponPool))
            {
                weaponPool = ProjectileWeaponManager.Instance.GetDefaultPool();
            }
            currentProjectileWeapon = weaponPool.Pop(new ProjectileWeaponData(gameObject, weaponAttachment, ammoPool)).GetComponent<ProjectileWeapon>();
        }

        private void InitAmmo()
        {
            if (!AmmoManager.Instance.TryGetPool(initialAmmoName, out currentAmmoPool))
            {
                currentAmmoPool = AmmoManager.Instance.GetDefaultPool();
            }
        }
    }
}