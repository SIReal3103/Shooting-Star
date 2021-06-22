using UnityEngine;

using ANTs.Template;

namespace ANTs.Core
{
    public class BulletPoolManager : Singleton<BulletPoolManager>
    {
        [Tooltip("The default bullet to load when other gunner need")]
        [SerializeField] BulletPool defaulBulletPool;

        private ExpandedDictionary<BulletPool> bulletPools;

        protected BulletPoolManager() { }

        private void Start()
        {
            bulletPools = new ExpandedDictionary<BulletPool>(gameObject);
        }

        public BulletPool GetNextBulletPool(BulletPool bulletPool)
        {
            return bulletPools.GetNextItem(bulletPool);
        }

        public BulletPool GetBulletPool(string key)
        {
            return bulletPools.GetValueFrom(key);
        }

        public BulletPool GetDefaultBulletPool()
        {
            return defaulBulletPool;
        }
    }
}