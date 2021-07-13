using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class ProjectileWeapon : Weapon
    {
        [Tooltip("The direction which bullets start firing")]
        [SerializeField] Transform[] projectileTransforms;

        private ANTsPool ammoPool;

        public ANTsPool GetAmmoPool() { return ammoPool; }
        public void SetAmmoPool(ANTsPool pool) { ammoPool = pool; }

        private void Awake()
        {
            gameObject.SetWakeUpDelegate(args =>
            {
                Init((ProjectileWeaponData)args);
            });
        }

        public override void Init(WeaponData data)
        {
            base.Init(data);
            ProjectileWeaponData pData = (ProjectileWeaponData)data;
            this.ammoPool = pData.ammoPool;
        }

        public override void TriggerWeapon()
        {
            if (ammoPool == null)
            {
                Debug.LogWarning("currentBulletPool is null.");
                return;
            }

            foreach (Transform projectileTransform in projectileTransforms)
            {
                ammoPool.Pop(new AmmoData(owner, projectileTransform.position, projectileTransform.up));
            }
        }

        public void ReturnToPool()
        {
            gameObject.ReturnToPoolOrDestroy();
        }

        private void OnDrawGizmos()
        {
            foreach (Transform projectileTransform in projectileTransforms)
            {
                Gizmos.DrawRay(new Ray(projectileTransform.position, projectileTransform.up));
            }
        }
    }

    public class ProjectileWeaponData : WeaponData
    {
        public ANTsPool ammoPool;

        public ProjectileWeaponData(GameObject owner, Transform parent, ANTsPool ammoPool)
            : base(owner, parent)
        {
            this.ammoPool = ammoPool;
        }
    }
}