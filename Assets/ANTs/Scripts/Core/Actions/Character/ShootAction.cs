using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class ShootAction : ActionBase
    {
        #region =================================================SERIALIZE_FIELD
        [SerializeField] Transform weaponAttachment;
        [SerializeField] float timeBetweenFire = 0.5f;
        [Tooltip("Initial gun type for gunner, default gun if null")]
        [SerializeField] ANTsPool initialProjectileWeaponPool;
        [Tooltip("Initial bullet type for gunner, default bullet if null")]
        [SerializeField] ANTsPool initialAmmoPool;
        #endregion



        #region =================================================VARIABLES
        private ANTsPool currentProjectileWeaponPool;
        private ANTsPool currentAmmoPool;
        private ProjectileWeaponControl currentGun;
        private float timeSinceLastFire = Mathf.Infinity;
        #endregion

        #region =================================================UNITY_EVENTS
        protected override void Start()
        {
            base.Start();

            //currentGunPool = initialGunPool ? initialGunPool : ProjectileWeaponManager.Instance.GetDefaultPool();
            //LoadCurrenGun();

            //currentBulletPool = initialBulletPool ? initialBulletPool : AmmoManager.Instance.GetDefaultPool();
            //LoadCurrentBullet();

            currentProjectileWeaponPool = initialProjectileWeaponPool;
            LoadnewGunAndDestroyCurrent();
            currentAmmoPool = initialAmmoPool;
            LoadBulletToCurrentGun();
        }
        #endregion



        #region =======================================IAction Implementation
        protected override void ActionUpdate()
        {
            FireBehaviour();
            UpdateTimer();
        }
        #endregion

        #region =================================================BEHAVIOURS
        private void FireBehaviour()
        {
            if (timeSinceLastFire > timeBetweenFire)
            {
                currentGun.Fire();
                timeSinceLastFire = 0;
            }
        }

        public void ChangeStrongerGun()
        {
            //if (ProjectileWeaponManager.Instance.ProgressNextPool(ref currentGunPool))
            //{
            //    LoadCurrenGun();
            //}
        }

        public void ChangeStrongerBullet()
        {
            //if (AmmoManager.Instance.ProgressNextPool(ref currentBulletPool))
            //{
            //    LoadCurrentBullet();
            //}
        }

        private void LoadnewGunAndDestroyCurrent()
        {
            if (currentGun != null) currentGun.ReturnToPool();
            currentGun = currentProjectileWeaponPool.Pop(new ProjectileWeaponData(weaponAttachment, gameObject, currentAmmoPool))
                .GetComponent<ProjectileWeaponControl>();
        }

        private void LoadBulletToCurrentGun()
        {
            currentGun.SetAmmoPool(currentAmmoPool);
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }
        #endregion

    }
}
