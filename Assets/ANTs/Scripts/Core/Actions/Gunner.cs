using UnityEngine;

namespace ANTs.Core
{
    public class Gunner : MonoBehaviour
    {
        private static string BULLET_SPAWN_POINT_PATH = "BulletSpawnPoint";

        [SerializeField] float timeBetweenFire = 0.5f;
        [Space]
        [Tooltip("Initial gun type for gunner")]
        [SerializeField] Gun initialGunPrefab;
        [Tooltip("Initial bullet type for gunner")]
        [SerializeField] BulletPool initialBulletPool;

        private Gun currentGun;
        private BulletPool currentBulletPool;
        private Transform bulletSpawnPoint;

        private float timeSinceLastFire = Mathf.Infinity;

        private void Start()
        {
            bulletSpawnPoint = transform.Find(BULLET_SPAWN_POINT_PATH);

            Gun gunPrefabToLoad = initialGunPrefab ? initialGunPrefab : GunManager.Instance.GetDefaultGunPrefab();
            LoadNewGunAndDestroyCurrent(gunPrefabToLoad);

            BulletPool bulletPoolToLoad = initialBulletPool ? initialBulletPool : BulletPoolManager.Instance.GetDefaultBulletPool();
            SetBulletPoolForCurrentGun(bulletPoolToLoad);
        }

        private void Update()
        {
            Fire();
            UpdateTimer();
        }

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
                BulletPoolManager.Instance.GetNextBulletPool(initialBulletPool)
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
            var position = bulletSpawnPoint.position;
            return new Vector2(position.x, position.y);
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }
    }
}
