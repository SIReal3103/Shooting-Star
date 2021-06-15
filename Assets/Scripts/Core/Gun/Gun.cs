using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class Gun : MonoBehaviour
    {
        /// <summary>
        /// Add this class to change gun by player.
        /// </summary>

        [Header("Gun")]
        /// Number of bullet firing
        public int NumberOfBullet;
        /// Direction of each bullet;
        [Tooltip("Direction of each bullet from left to right")]
        public List<Vector2> BulletDirectionList = new List<Vector2>();
        /// Bullet that gun firing;
        public BulletPool BulletPrefab;
        

        [HideInInspector]
        public Character _character;

        public void Firing()
        {
            for (int i = 0; i < NumberOfBullet; i++)
            {
                BulletData bulletData = new BulletData(gameObject, _character.GetBulletSpawnPosition(), BulletDirectionList[i]);
                BulletPrefab.Pop().InitData(bulletData);
            }
        }
    }
}