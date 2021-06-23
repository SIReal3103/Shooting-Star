using UnityEngine;

using ANTs.Template;
using System.Collections.Generic;

namespace ANTs.Core
{
    public class BulletPoolManager : Singleton<BulletPoolManager>
    {
        [SerializeField] Bullet.BulletIdentifier defaultId;

        public BulletPool GetNextBulletPoolPrefab(BulletPool bulletPool)
        {
            BulletPool result;
            if(bulletPools.TryGetValue(bulletPool.NextId, out result))
            {
                return result;
            }
            Debug.Log("Bullet level max");
            return bulletPool;
        }
        public BulletPool GetDefaultBulletPoolPrefab()
        {
            return bulletPools[defaultId];
        }

        [SerializeField] List<Bullet> bulletPrefabs;

        private Dictionary<Bullet.BulletIdentifier, BulletPool> bulletPools = 
            new Dictionary<Bullet.BulletIdentifier, BulletPool>();

        protected BulletPoolManager() { }

        private void Start()
        {
            foreach(Bullet bulletPrefab in bulletPrefabs)
            {
                BulletPool bulletPool = CreateBulletPool(bulletPrefab);
                bulletPool.Prefab = bulletPrefab;
                bulletPools.Add(bulletPool.Id, bulletPool);
            }
        }

        private BulletPool CreateBulletPool(Bullet bullet)
        {
            GameObject go = new GameObject(bullet.name + "_pool");
            go.transform.SetParent(transform);
            return go.AddComponent<BulletPool>();
        }
    }
}