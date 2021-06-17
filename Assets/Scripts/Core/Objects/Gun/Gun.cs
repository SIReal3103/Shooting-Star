using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] Vector2[] bulletDirections;

        private Gunner gunHolder;

        public void Init(Gunner gunHolder, BulletPool bulletPool)
        {
            this.gunHolder = gunHolder;
        }

        public void Fire(BulletPool bulletPool)
        {
            for (int i = 0; i < bulletDirections.Length; i++)
            {
                BulletData bulletData = new BulletData(gameObject, gunHolder.GetBulletSpawnPosition(), bulletDirections[i]);
                bulletPool.Pop(bulletData);
            }
        }
    }
}