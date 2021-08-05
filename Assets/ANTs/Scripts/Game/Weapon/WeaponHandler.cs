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
        [SerializeField] float weaponAttachmentRotateSpeed = 5f;
        [SerializeField] float rotationAcceptableRange = 0.01f;

        private LazyANTs<Weapon> currentWeapon;
        private LazyANTs<ProjectileWeapon> currentProjectileWeapon = null;
        private LazyANTs<MeleeWeapon> currentMeleeWeapon = null;
        private LazyANTs<ANTsPool> currentAmmoPool = null;

        private void Awake()
        {
            if (initialWeaponIsMelee)
            {
                currentMeleeWeapon = new LazyANTs<MeleeWeapon>(GetDefaultMeleeWeapon);
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

        public bool DirectWeaponAttachmentTo(Vector2 targetPosition)
        {
            if (TryGetComponent(out MoveAction move))
            {
                move.ChangeFacingDirection(targetPosition);
            }

            Vector2 targetDirection = targetPosition - (Vector2)transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(
                transform.position.x > targetPosition.x ? Vector3.back : Vector3.forward,
                targetDirection);

            targetRotation = Quaternion.Euler(
                targetRotation.eulerAngles.x,
                targetRotation.eulerAngles.y,
                targetRotation.eulerAngles.z + 90);

            weaponAttachment.rotation = Quaternion.Slerp(weaponAttachment.rotation, targetRotation, weaponAttachmentRotateSpeed * Time.deltaTime);
            return 1f - Mathf.Abs(Quaternion.Dot(weaponAttachment.rotation, targetRotation)) < rotationAcceptableRange;
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

        private MeleeWeapon GetDefaultMeleeWeapon()
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