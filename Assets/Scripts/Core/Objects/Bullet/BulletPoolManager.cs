using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class BulletPoolManager : Singleton<BulletPoolManager>
    {
        protected BulletPoolManager() { }

        [SerializeField] string DEFAULT_GUN_KEY = "BulletPoolLevel1";

        ExpandedDictionary<BulletPool> bulletPools;

        private void Start()
        {
            bulletPools = new ExpandedDictionary<BulletPool>(gameObject);
        }

        public BulletPool GetBulletPool(string s)
        {
            return bulletPools.GetValueFrom(s);
        }

        public BulletPool GetBulletPool()
        {
            return bulletPools.GetValueFrom(DEFAULT_GUN_KEY);
        }
    }
}