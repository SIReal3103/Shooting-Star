using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] BulletPool BulletPool;
        [SerializeField] Vector2[] bulletDirections;

        [HideInInspector] private Gunner gunHolder;

        public void Init(Gunner gunHolder)
        {
            this.gunHolder = gunHolder;
        }

        public void Fire()
        {
            for (int i = 0; i < bulletDirections.Length; i++)
            {
                BulletData bulletData = new BulletData(gameObject, gunHolder.GetBulletSpawnPosition(), bulletDirections[i]);
                BulletPool.Pop().InitData(bulletData);
            }
        }
    }
}