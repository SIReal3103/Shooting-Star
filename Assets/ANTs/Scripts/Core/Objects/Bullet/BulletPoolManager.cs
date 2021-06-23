using UnityEngine;

using ANTs.Template;
using System.Collections.Generic;

namespace ANTs.Core
{
    public class BulletPoolManager : PoolManager<BulletPoolManager>
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
    }
}