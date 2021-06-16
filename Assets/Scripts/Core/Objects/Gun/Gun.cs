using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] Vector2[] bulletDirections;
        [SerializeField] BulletPool BulletPool;
        Gunner gunHolder;

        public void Init(Gunner gunHolder)
        {
            if(BulletPool == null)
                BulletPool = BulletPoolManager.Instance.GetBulletPool();

            this.gunHolder = gunHolder;
        }

        public void Fire()
        {
            for (int i = 0; i < bulletDirections.Length; i++)
            {
                BulletData bulletData = new BulletData(gameObject, gunHolder.GetBulletSpawnPosition(), bulletDirections[i]);
                BulletPool.Pop().InitData(bulletData);
            }
        }
    }
}