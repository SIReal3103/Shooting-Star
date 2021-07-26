using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [Space(20)]
        [SerializeField] bool initialWeaponIsMelee = true;
        [SerializeField] string initialWeaponName;
        [Conditional("initialWeaponIsMelee", false)]
        [SerializeField] string initialAmmoName;

        private LazyANTs<Weapon> currentWeapon;
        private LazyANTs<ProjectileWeapon> currentProjectileWeapon = null;
        private LazyANTs<MeleeWeapon> currentMeleeWeapon = null;
        private LazyANTs<ANTsPool> currentAmmoPool = null;

        private void Awake()
        {
            if (initialWeaponIsMelee)
            {
                currentMeleeWeapon = new LazyANTs<MeleeWeapon>(InitMeleeWeapon);
                currentWeapon = new LazyANTs<Weapon>(() => currentMeleeWeapon.value);
            }
            else
            {
                currentAmmoPool = new LazyANTs<ANTsPool>(GetDefaultAmmo);
                currentProjectileWeapon = new LazyANTs<ProjectileWeapon>(GetDefaultProjectileWeapon);
                currentWeapon = new LazyANTs<Weapon>(() => currentProjectileWeapon.value);
            }
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }

        public void TriggerCurrentWeapon()
        {
            currentWeapon.value.TriggerWeapon();
        }

        public void TriggerMeleeWeapon()
        {
            currentMeleeWeapon.value.TriggerWeapon();
        }

        public void TriggerProjectileWeapon()
        {
            currentProjectileWeapon.value.TriggerWeapon();
        }

        public void NotifyWeaponOwnerDie()
        {
            currentWeapon.value.OwnerDie();
        }

        public void DirectWeaponAttachmentTo(Vector2 position)
        {
            Debug.Log(weaponAttachment.rotation.eulerAngles);
            weaponAttachment.right = position - (Vector2)transform.position;
        }

        public void UpgradeProjectileWeapon()
        {
            Weapon weaponToUpgrade = currentProjectileWeapon.value;
            WeaponUpgradeHandler.UpgradeWeapon(ref weaponToUpgrade);
            currentProjectileWeapon.value = (ProjectileWeapon)weaponToUpgrade;
        }

        public void UpgradeCurrentAmmo()
        {
            WeaponUpgradeHandler.UpgradeWeaponAmmo(currentProjectileWeapon.value, ref currentAmmoPool.refValue);
        }

        private MeleeWeapon InitMeleeWeapon()
        {
            if (!MeleeWeaponManager.Instance.TryGetPool(initialWeaponName, out ANTsPool weaponPool))
            {
                weaponPool = MeleeWeaponManager.Instance.GetDefaultPool();
            }
            return weaponPool.Pop(new MeleeWeaponData(gameObject, weaponAttachment)).GetComponent<MeleeWeapon>();
        }

        private ProjectileWeapon GetDefaultProjectileWeapon()
        {
            if (!ProjectileWeaponManager.Instance.TryGetPool(initialWeaponName, out ANTsPool weaponPool))
            {
                weaponPool = ProjectileWeaponManager.Instance.GetDefaultPool();
            }
            return weaponPool.Pop(new ProjectileWeaponData(gameObject, weaponAttachment, currentAmmoPool.value)).GetComponent<ProjectileWeapon>();
        }

        private ANTsPool GetDefaultAmmo()
        {
            if (!AmmoManager.Instance.TryGetPool(initialAmmoName, out ANTsPool ammoPool))
            {
                ammoPool = AmmoManager.Instance.GetDefaultPool();
            }
            return ammoPool;
        }
    }
}