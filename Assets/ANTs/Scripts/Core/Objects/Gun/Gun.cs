using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class Gun : MonoBehaviour, IANTsPoolable<GunPool, Gun>, IProgressable
    {

        [Tooltip("The direction which bullet start firing")]
        [SerializeField] Vector2[] bulletDirections;
        [Space]
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;

        private GunnerAction gunHolder;
        private BulletPool currentBulletPool;

        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }
        public GunPool CurrentPool { get; set; } // IANTsPoolable Implementation

        public void SetBulletPool(BulletPool pool) => currentBulletPool = pool;

        public void Init(GunnerAction gunHolder)
        {
            this.gunHolder = gunHolder;
        }

        public void Fire()
        {
            if (currentBulletPool == null)
                throw new UnityException("CurrentBulletPool can't be null, (BulletPoolManager might be empty?)");

            for (int i = 0; i < bulletDirections.Length; i++)
            {
                float rotationAngle = gunHolder.transform.rotation.eulerAngles.z;
                Vector2 bulletDirection = Quaternion.AngleAxis(rotationAngle, Vector3.forward) * bulletDirections[i];

                BulletData bulletData = new BulletData(gunHolder.gameObject, gunHolder.GetBulletSpawnPosition(), bulletDirection);
                currentBulletPool.Pop(bulletData);
            }
        }

        #region IANTsPoolable IMPLEMENTATION
        public void ReturnToPool()
        {
            CurrentPool.ReturnToPool(this);
        }

        public void WakeUp(object args)
        {
            gameObject.SetActive(true);

            GunData data = args as GunData;
            transform.SetParent(data.transform);
            gunHolder = data.gunHolder;
            currentBulletPool = data.bulletPool;

            transform.localPosition = Vector3.zero;
        }

        public void Sleep()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}