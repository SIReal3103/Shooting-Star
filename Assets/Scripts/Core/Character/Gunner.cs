using UnityEngine;

using Game.Event;

namespace Game.Core
{
    [RequireComponent(typeof(Character))]
    public class Gunner : MonoBehaviour
    {
        public static string BULLET_SPAWN_POINT_PATH = "BulletSpawnPoint";

        [SerializeField] float timeBetweenFire = 0.5f;
        [SerializeField] Gun initialGunPrefab;

        Gun currentGun;
        [SerializeField] BulletPool currentBulletPool;

        Transform bulletSpawnPoint;

        float timeSinceLastFire = Mathf.Infinity;

        private void Start()
        {
            bulletSpawnPoint = transform.Find(BULLET_SPAWN_POINT_PATH);

            ChangeNewGunAndDestroyCurrent(initialGunPrefab);
        }

        private void Update()
        {
            FireBehaviour();

            UpdateTimer();
        }

        public void ChangeToStrongerGun()
        {
            ChangeNewGunAndDestroyCurrent(GunManager.Instance.GetStrongerGunFrom(currentGun));
        }

        private void ChangeNewGunAndDestroyCurrent(Gun gunPrefab)
        {
            if (currentGun != null) Destroy(currentGun);

            currentGun = Instantiate(gunPrefab, transform);
            // Remove (Clone) trailing
            currentGun.name = gunPrefab.name;
            currentGun.Init(this, currentBulletPool);
        }

        public void FireBehaviour()
        {
            if (timeSinceLastFire > timeBetweenFire)
            {
                currentGun.Fire();
                timeSinceLastFire = 0;
            }
        }

        public Vector2 GetBulletSpawnPosition()
        {
            return new Vector2(bulletSpawnPoint.position.x, bulletSpawnPoint.position.y);
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }
    } 
}
