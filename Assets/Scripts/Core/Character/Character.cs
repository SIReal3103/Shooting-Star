using UnityEngine;

using Game.Combat;
using Game.Movement;

namespace Game.Core
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(PlayerBehaviour))]
    public class Character : MonoBehaviour
    {
        public static string BULLET_SPAWN_POINT_PATH = "BulletSpawnPoint";

        Transform bulletSpawnPoint;

        private void Start()
        {
            bulletSpawnPoint = transform.Find(BULLET_SPAWN_POINT_PATH);
        }

        public Vector2 GetBulletSpawnPosition()
        {
            return new Vector2(bulletSpawnPoint.position.x, bulletSpawnPoint.position.y);
        }
    }
}