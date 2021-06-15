using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [RequireComponent(typeof(Character))]
    public class Gunner : MonoBehaviour
    {
        public static string BULLET_SPAWN_POINT_PATH = "BulletSpawnPoint";

        [SerializeField] float timeBetweenFire = 0.5f;
        [SerializeField] Gun initialGunPrefab;

        Gun currentGun;
        Transform bulletSpawnPoint;

        float timeSinceLastFire = Mathf.Infinity;

        private void Start()
        {
            bulletSpawnPoint = transform.Find(BULLET_SPAWN_POINT_PATH);
            currentGun = initialGunPrefab.GetComponent<Gun>();

            ChangeNewGunAndDestroyCurrent(initialGunPrefab);
        }

        private void Update()
        {
            FireBehaviour();

            UpdateTimer();
        }

        public void ChangeNewGunAndDestroyCurrent(Gun gunPrefab)
        {
            if (currentGun != null) Destroy(currentGun.gameObject);
            currentGun = Instantiate(gunPrefab, transform);
            currentGun.Init(this);
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
