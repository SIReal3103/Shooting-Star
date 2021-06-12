using UnityEngine;

using Game.Template;

namespace Game.Core
{
    public class BulletPool : Pool<BulletPool, BulletObject, Ray2D>
    {

    }

    public class BulletObject : PoolObject<BulletPool, BulletObject, Ray2D>
    {
        public override void Init(Ray2D ray)
        {
            Bullet bullet = instance.GetComponent<Bullet>();

            bullet.transform.position = ray.origin;
            bullet.moveDirection = ray.direction;
        }

        protected override void WakeUp()
        {
            instance.SetActive(true);            
        }

        protected override void Sleep()
        {
            instance.SetActive(false);
        }
    }
}