using UnityEngine;
using ANTs.Template;
using System.Collections.Generic;

namespace ANTs.Core
{
    public class PoolManager<TPool> : Singleton<TPool>
        where TPool : Component
    {
        [SerializeField] List<Bullet> bulletPrefabs;

        protected private Dictionary<Bullet.BulletIdentifier, BulletPool> bulletPools =
            new Dictionary<Bullet.BulletIdentifier, BulletPool>();

        private BulletPool CreateBulletPool(Bullet bullet)
        {
            GameObject go = new GameObject(bullet.name + "_pool");
            go.transform.SetParent(transform);
            return go.AddComponent<BulletPool>();
        }

        protected PoolManager() { }

        private void Start()
        {
            foreach (Bullet bulletPrefab in bulletPrefabs)
            {
                BulletPool bulletPool = CreateBulletPool(bulletPrefab);
                bulletPool.Prefab = bulletPrefab;
                bulletPools.Add(bulletPool.Id, bulletPool);
            }
        }
    }
}