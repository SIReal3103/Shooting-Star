using UnityEngine;

namespace ANTs.Game
{
    public class Gun : MonoBehaviour
    {
        [Tooltip("The direction which bullet start firing")]
        [SerializeField] Vector2[] bulletDirections;

        private Gunner gunHolder;
        private BulletPool currentBulletPool;

        public void SetBulletPool(BulletPool pool) => currentBulletPool = pool;

        public void Init(Gunner gunHolder)
        {
            this.gunHolder = gunHolder;
        }

        public void Fire()
        {
            if(currentBulletPool == null)
                throw new UnityException("currentBulletPool can't be null");

            for (int i = 0; i < bulletDirections.Length; i++)
            {
                BulletData bulletData = new BulletData(gameObject, gunHolder.GetBulletSpawnPosition(), bulletDirections[i]);
                currentBulletPool.Pop(bulletData);
            }
        }
    }
}