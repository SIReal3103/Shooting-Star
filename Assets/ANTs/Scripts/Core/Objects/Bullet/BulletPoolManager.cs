using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class BulletPoolManager : ProgressablePoolManager<BulletPoolManager, BulletPool, Bullet>
    {

    }

    public class BulletPool : ANTsPool<BulletPool, Bullet>
    {
    }

    public class BulletData
    {
        public Vector2 origin;
        public Vector2 moveDirection;
        public GameObject source;

        public BulletData(GameObject source, Vector2 origin, Vector2 moveDirection)
        {
            this.source = source;
            this.origin = origin;
            this.moveDirection = moveDirection;
        }
    }
}