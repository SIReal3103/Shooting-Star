using UnityEngine;

using Game.Template;

namespace Game.Core
{
    public class BulletPool : Pool<BulletPool, BulletObject, BulletData>
    {

    }


    public class BulletObject : PoolObject<BulletPool, BulletObject, BulletData>
    {
        public override void Init(BulletData data)
        {
            Bullet bullet = instance.GetComponent<Bullet>();

            bullet.transform.position = data.origin;
            bullet.moveDirection = data.moveDirection;
            bullet.source = data.source;
            bullet.bulletObject = this;
        }

        public override void WakeUp()
        {
            instance.SetActive(true);
        }

        public override void Sleep()
        {
            instance.SetActive(false);
        }
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