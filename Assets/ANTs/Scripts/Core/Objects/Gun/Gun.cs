using UnityEngine;

namespace ANTs.Core
{
    public class Gun : MonoBehaviour
    {
        [System.Serializable]
        enum GunIdentifier
        {
            GunLevel1,
            GunLevel2,
            GunLevel3,
            GunLevel4,
            None
        }

        [Tooltip("The direction which bullet start firing")]
        [SerializeField] Vector2[] bulletDirections;
        [Space]
        [SerializeField] GunIdentifier gunId;
        [SerializeField] GunIdentifier nextGunId;

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
                throw new UnityException("CurrentBulletPool can't be null");

            for (int i = 0; i < bulletDirections.Length; i++)
            {
                float rotationAngle = gunHolder.transform.rotation.eulerAngles.z;
                Vector2 bulletDirection = Quaternion.AngleAxis(rotationAngle, Vector3.forward) * bulletDirections[i];

                BulletData bulletData = new BulletData(gunHolder.gameObject, gunHolder.GetBulletSpawnPosition(), bulletDirection);
                currentBulletPool.Pop(bulletData);
            }
        }
    }
}