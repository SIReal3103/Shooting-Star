using UnityEngine;

namespace ANTs.Core
{
    public class Gunner : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] Transform bulletSpawnPosition;
        [Space]
        [SerializeField] float timeBetweenFire = 0.5f;
        [Space]
        [Tooltip("Initial gun type for gunner")]
        [SerializeField] Gun initialGunPrefab;
        [Tooltip("Initial bullet type for gunner")]
        [SerializeField] BulletPool initialBulletPool;
        #endregion

        #region Variables
        private Gun currentGun;
        private BulletPool currentBulletPool;

        private float timeSinceLastFire = Mathf.Infinity;
        #endregion



        #region Unity Events
        private void Start()
        {
            Gun gunPrefabToLoad = initialGunPrefab ? initialGunPrefab : GunManager.Instance.GetDefaultGunPrefab();
            LoadNewGunAndDestroyCurrent(gunPrefabToLoad);

            BulletPool bulletPoolToLoad = initialBulletPool ? initialBulletPool : BulletPoolManager.Instance.GetDefaultPool();
            SetBulletPoolForCurrentGun(bulletPoolToLoad);
        }

        private void Update()
        {
            Fire();
            UpdateTimer();
        }
        #endregion

        #region Behaviours
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
            LoadNewGunAndDestroyCurrent(GunManager.Instance.GetNextGun(currentGun));
        }

        public void ChangeStrongerBullet()
        {
            SetBulletPoolForCurrentGun(
                BulletPoolManager.Instance.ProgressNextPool(currentBulletPool)
                );
        }

        private void LoadNewGunAndDestroyCurrent(Gun gunPrefab)
        {
            if (currentGun != null) Destroy(currentGun);

            currentGun = Instantiate(gunPrefab, transform);

            // Remove trailing (Clone) 
            currentGun.name = gunPrefab.name;

            currentGun.Init(this);
            SetBulletPoolForCurrentGun(currentBulletPool);
        }

        private void SetBulletPoolForCurrentGun(BulletPool bulletPoolToLoad)
        {
            currentBulletPool = bulletPoolToLoad;
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
