using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class Gun : MonoBehaviour, IANTsPoolable<GunPool, Gun>, IProgressable
    {

        [Tooltip("The direction which bullet start firing")]
        [SerializeField] Transform[] projectileTransforms;
        [Header("IProgressable")]
        [Space(10)]
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;

        private GameObject source;
        private BulletPool currentBulletPool;

        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }
        public GunPool CurrentPool { get; set; } // IANTsPoolable Implementation

        #region ==================================Behaviours
        public void SetBulletPool(BulletPool pool) => currentBulletPool = pool;
        public void Fire()
        {
            if (currentBulletPool == null)
                throw new UnityException("CurrentBulletPool can't be null, (BulletPoolManager might be empty?)");

            foreach (Transform projectileTransform in projectileTransforms)
            {
                currentBulletPool.Pop(new BulletData(source.gameObject, projectileTransform.position, projectileTransform.up));
            }
        }
        #endregion

        #region ============================ IANTsPoolable
        public void ReturnToPool()
        {
            CurrentPool.ReturnToPool(this);
        }

        public void WakeUp(object args)
        {
            gameObject.SetActive(true);

            GunData data = args as GunData;
            transform.SetParent(data.parent);
            source = data.source;
            currentBulletPool = data.bulletPool;

            transform.localPosition = Vector3.zero;
        }

        public void Sleep()
        {
            gameObject.SetActive(false);
        }
        #endregion


        #region =================================projectileTransforms
        private void OnDrawGizmos()
        {
            foreach (Transform projectileTransform in projectileTransforms)
            {
                Gizmos.DrawRay(new Ray(projectileTransform.position, projectileTransform.up));
            }
        }
        #endregion
    }
}