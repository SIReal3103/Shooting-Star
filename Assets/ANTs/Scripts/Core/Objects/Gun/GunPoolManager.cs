﻿using System.Collections;
using UnityEngine;
using ANTs.Template;

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
        public Transform transform;
        public GunnerAction gunHolder;
        public BulletPool bulletPool;

        public GunData(Transform transform, GunnerAction gunHolder, BulletPool bullet)
        {
            this.transform = transform;
            this.gunHolder = gunHolder;
            this.bulletPool = bullet;
        }
    }
}