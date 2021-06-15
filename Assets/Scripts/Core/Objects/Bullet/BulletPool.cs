﻿using UnityEngine;

using Game.Template;

namespace Game.Core
{
    public class BulletPool : Pool<BulletPool, BulletObject, BulletData>
    {

    }


    public class BulletObject : PoolObject<BulletPool, BulletObject, BulletData>
    {
        public override void InitData(BulletData data)
        {
            Bullet bullet = GetInstance().GetComponent<Bullet>();
            AssignData(bullet, data);
        }

        private void AssignData(Bullet bullet, BulletData data)
        {
            bullet.transform.position = data.origin;
            bullet.SetDirection(data.moveDirection);
            bullet.source = data.source;

            bullet.bulletObject = this;
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