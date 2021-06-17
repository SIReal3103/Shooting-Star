using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class BulletPoolManager : Singleton<BulletPoolManager>
    {
        protected BulletPoolManager() { }

        [SerializeField] string DEFAULT_BULLET_KEY = "BulletPoolLevel1";

        ExpandedDictionary<BulletPool> bulletPools;

        private void Start()
        {
            bulletPools = new ExpandedDictionary<BulletPool>(gameObject);
        }

        public BulletPool GetNextBulletPool(BulletPool bulletPool)
        {
            return bulletPools.GetNextItem(bulletPool);
        }

        public BulletPool GetBulletPool(string s)
        {
            return bulletPools.GetValueFrom(s);
        }

        public BulletPool GetBulletPool()
        {
            return bulletPools.GetValueFrom(DEFAULT_BULLET_KEY);
        }
    }
}