using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class BulletPoolManager : Singleton<BulletPoolManager>
    {
        protected BulletPoolManager() { }

        public string DEFAULT_GUN_KEY = "";

        private Dictionary<string, BulletPool> bulletPools = new Dictionary<string, BulletPool>();

        private void Start()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                bulletPools.Add(child.name, child.GetComponent<BulletPool>());

                if (DEFAULT_GUN_KEY == "") DEFAULT_GUN_KEY = child.name;
            }
        }

        public BulletPool GetBulletPool(string s)
        {
            BulletPool pool;
            if(bulletPools.TryGetValue(s, out pool))
            {
                throw new UnityException("Bullet pool key not found");
            }
            return pool;
        }

        public BulletPool GetBulletPool()
        {
            BulletPool pool;
            if(!bulletPools.TryGetValue(DEFAULT_GUN_KEY, out pool))
            {
                throw new UnityException("Default gun key is invalid");
            }
            return pool;
        }
    }
}