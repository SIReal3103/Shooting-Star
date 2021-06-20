using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(BulletPool))]
    public class Gunner : MonoBehaviour
    {
        private static string BULLET_SPAWN_POINT_PATH = "BulletSpawnPoint";

        [SerializeField] float timeBetweenFire = 0.5f;
        [Space]
        [Tooltip("Initial gun type for gunner")]
        [SerializeField] Gun initialGunPrefab;
        [Tooltip("Initial bullet type for gunner")]
        [SerializeField] BulletPool initialBulletPool;

        float timeSinceLastFire = Mathf.Infinity;
        Gun currentGun;
        
        BulletPool currentBulletPool;

        Transform bulletSpawnPoint;

        private void Start()
        {
            bulletSpawnPoint = transform.Find(BULLET_SPAWN_POINT_PATH);

            Gun gunPrefabToLoad = initialGunPrefab ? initialGunPrefab : GunManager.Instance.GetDefaultGunPrefab();
            LoadNewGunAndDestroyCurrent(gunPrefabToLoad);

            BulletPool bulletPoolToLoad = initialBulletPool ? initialBulletPool : BulletPoolManager.Instance.GetDefaultBulletPool();
            currentBulletPool = bulletPoolToLoad;
            currentGun.SetBulletPool(currentBulletPool);
        }

        private void Update()
        {
            Fire();
            UpdateTimer();
        }

        public void ChangeStrongerGun()
        {
            LoadNewGunAndDestroyCurrent(GunManager.Instance.GetNextGun(currentGun));
        }

        public void ChangeStrongerBullet()
        {
            currentBulletPool = BulletPoolManager.Instance.GetNextBulletPool(initialBulletPool);
            currentGun.SetBulletPool(currentBulletPool);
        }

        private void LoadNewGunAndDestroyCurrent(Gun gunPrefab)
        {
            if (currentGun != null) Destroy(currentGun);

            currentGun = Instantiate(gunPrefab, transform);
            // Remove trailing (Clone) 
            currentGun.name = gunPrefab.name;
            currentGun.Init(this, currentBulletPool);
        }

        public void Fire()
        {
            if (timeSinceLastFire > timeBetweenFire)
            {
                currentGun.Fire();
                timeSinceLastFire = 0;
            }
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
