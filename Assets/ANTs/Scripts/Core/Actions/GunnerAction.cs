using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class GunnerAction : ActionBase
    {
        #region =================================================SERIALIZE_FIELD
        [SerializeField] Transform bulletSpawnPosition;
        [Space]
        [SerializeField] float timeBetweenFire = 0.5f;
        [Space]
        [Tooltip("Initial gun type for gunner, default gun if null")]
        [SerializeField] GunPool initialGunPool;
        [Tooltip("Initial bullet type for gunner, default bullet if null")]
        [SerializeField] BulletPool initialBulletPool;
        #endregion



        #region =================================================VARIABLES
        private GunPool currentGunPool;
        private BulletPool currentBulletPool;
        private Gun currentGun;
        private float timeSinceLastFire = Mathf.Infinity;
        #endregion

        #region =================================================UNITY_EVENTS
        protected override void Start()
        {
            base.Start();

            currentGunPool = initialGunPool ? initialGunPool : GunPoolManager.Instance.GetDefaultPool();
            LoadCurrenGun();

            currentBulletPool = initialBulletPool ? initialBulletPool : BulletPoolManager.Instance.GetDefaultPool();
            LoadCurrentBullet();
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
            if(GunPoolManager.Instance.ProgressNextPool(ref currentGunPool))
            {
                LoadCurrenGun();
            }
        }

        public void ChangeStrongerBullet()
        {
            if(BulletPoolManager.Instance.ProgressNextPool(ref currentBulletPool))
            {
                LoadCurrentBullet();
            }
        }

        private void LoadCurrenGun()
        {
            if(currentGun != null) currentGun.ReturnToPool();
            currentGun = currentGunPool.Pop(new GunData(transform, this, currentBulletPool));
        }

        private void LoadCurrentBullet()
        {
            currentGun.SetBulletPool(currentBulletPool);
        }

        public Vector2 GetBulletSpawnPosition()
        {
            var position = bulletSpawnPosition.position;
            return new Vector2(position.x, position.y);
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }
        #endregion

    }
}
