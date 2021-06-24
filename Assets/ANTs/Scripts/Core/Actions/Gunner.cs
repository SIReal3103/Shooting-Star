using UnityEngine;

namespace ANTs.Core
{
    public class Gunner : MonoBehaviour
    {
        #region =================================================SERIALIZE_FIELD
        [SerializeField] Transform bulletSpawnPosition;
        [Space]
        [SerializeField] float timeBetweenFire = 0.5f;
        [Space]
        [Tooltip("Initial gun type for gunner")]
        [SerializeField] GunPool initialGunPool;
        [Tooltip("Initial bullet type for gunner")]
        [SerializeField] BulletPool initialBulletPool;
        #endregion


        #region =================================================VARIABLES
        private GunPool currentGunPool;
        private BulletPool currentBulletPool;

        private Gun currentGun;
        private float timeSinceLastFire = Mathf.Infinity;
        #endregion


        #region =================================================UNITY_EVENTS
        private void Start()
        {
            currentGunPool = initialGunPool ? initialGunPool : GunPoolManager.Instance.GetDefaultPool();
            LoadCurrenGun();

            currentBulletPool = initialBulletPool ? initialBulletPool : BulletPoolManager.Instance.GetDefaultPool();
            LoadCurrentBullet();
        }

        private void Update()
        {
            Fire();
            UpdateTimer();
        }
        #endregion


        #region =================================================BEHAVIOURS
        public void Fire()
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
    }
    #endregion
}
