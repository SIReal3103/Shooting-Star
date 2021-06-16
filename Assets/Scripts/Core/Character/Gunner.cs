﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Event;

namespace Game.Core
{
    [RequireComponent(typeof(Character))]
    public class Gunner : MonoBehaviour, MMEventListener<ChangeWeaponEvent>
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

            ChangeNewGunAndDestroyCurrent(initialGunPrefab);
        }

        private void Update()
        {
            FireBehaviour();

            UpdateTimer();
        }

        public void ChangeNewGunAndDestroyCurrent(Gun gunPrefab)
        {
            if (currentGun != null) Destroy(currentGun);

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

        public void OnMMEvent(ChangeWeaponEvent eventType)
        {
            ChangeNewGunAndDestroyCurrent(eventType.Gun.GetComponent<Gun>());
        }

        private void OnEnable()
        {
            this.MMEventStartListening<ChangeWeaponEvent>();
        }

        private void OnDisable()
        {
            this.MMEventStopListening<ChangeWeaponEvent>();
        }        
    }
    
}
