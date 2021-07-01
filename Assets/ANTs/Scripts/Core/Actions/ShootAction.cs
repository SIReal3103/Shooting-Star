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
        [SerializeField] ProjectileWeaponPool initialGunPool;
        [Tooltip("Initial bullet type for gunner, default bullet if null")]
        [SerializeField] AmmoPool initialBulletPool;
        #endregion



        #region =================================================VARIABLES
        private ProjectileWeaponPool currentGunPool;
        private AmmoPool currentBulletPool;
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

            currentGunPool = initialGunPool;
            LoadnewGunAndDestroyCurrent();
            currentBulletPool = initialBulletPool;
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
            currentGun = currentGunPool.Pop(new ProjectileWeaponData(weaponAttachment, gameObject, currentBulletPool))
                .GetComponent<ProjectileWeaponControl>();
        }

        private void LoadBulletToCurrentGun()
        {
            currentGun.SetAmmoPool(currentBulletPool);
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }
        #endregion

    }
}
