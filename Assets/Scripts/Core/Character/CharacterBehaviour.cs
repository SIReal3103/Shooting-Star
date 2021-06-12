using UnityEngine;

using Game.Combat;
using Game.Movement;

namespace Game.Core
{
    [RequireComponent(typeof(Damagable))]
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(Mover))]
    public class CharacterBehaviour : MonoBehaviour
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