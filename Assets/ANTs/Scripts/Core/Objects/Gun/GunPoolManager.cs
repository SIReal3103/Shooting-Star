using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class GunPoolManager : ProgressablePoolManager<GunPoolManager, GunPool, Gun>
    {

    }

    public class GunPool : ANTsPool<GunPool, Gun>
    {

    }

    public class GunData
    {
        public Transform parent;
        public GameObject source;
        public BulletPool bulletPool;

        public GunData(Transform transform, GameObject source, BulletPool bulletPool)
        {
            this.parent = transform;
            this.source = source;
            this.bulletPool = bulletPool;
        }
    }
}