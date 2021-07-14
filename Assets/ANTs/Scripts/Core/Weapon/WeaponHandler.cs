﻿using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [Space(20)]
        [SerializeField] bool initialWeaponIsMelee = true;
        [SerializeField] string initialWeaponName;
        [Conditional("initialWeaponIsMelee", false)]
        [SerializeField] string initialAmmoName;

        [ReadOnly]
        public LazyANTs<Weapon> currentWeapon;

        private ProjectileWeapon currentProjectileWeapon;
        private MeleeWeapon currentMeleeWeapon;
        private ANTsPool currentAmmoPool;


        private void Awake()
        {
            currentWeapon = new LazyANTs<Weapon>(null);
        }

        private void Start()
        {
            if (initialWeaponIsMelee)
            {
                InitMeleeWeapon();
                currentWeapon.value = currentMeleeWeapon;
            }
            else
            {
                InitAmmo();
                InitProjectileWeapon(currentAmmoPool);
                currentWeapon.value = currentProjectileWeapon;
            }
        }

        #region ==================================== TRIGGERS
        public void TriggerCurrentWeapon()
        {
            currentWeapon.value.TriggerWeapon();
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

        public void DirectWeaponTo(Vector2 position)
        {
            Vector2 delt = position - (Vector2)transform.position;
            float ang = Mathf.Atan2(delt.y, delt.x) * Mathf.Rad2Deg;
            weaponAttachment.eulerAngles = new Vector3(0, 0, ang - 90f);
        }

        public void UpgradeProjectileWeapon()
        {
            Weapon weaponToUpgrade = currentProjectileWeapon;
            WeaponUpgradeHandler.UpgradeWeapon(ref weaponToUpgrade);
            currentProjectileWeapon = (ProjectileWeapon)weaponToUpgrade;
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