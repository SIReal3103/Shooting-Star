using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class ProjectileWeapon : Weapon
    {
        [Tooltip("The direction which bullet start firing")]
        [SerializeField] Transform[] projectileTransforms;

        private ANTsPool currentAmmo;

        public ANTsPool GetCurrentAmmoPool() { return currentAmmo; }


        private void Awake()
        {
            gameObject.SetWakeUpDelegate(args =>
            {
                ProjectileWeaponData data = args as ProjectileWeaponData;
                currentAmmo = data.ammoPool;
                transform.SetParentPreserve(data.parent);
                owner = data.owner;
            });
        }

        public void SetAmmoPool(ANTsPool pool) { currentAmmo = pool; }

        public override void TriggerWeapon()
        {
            if (currentAmmo == null)
            {
                Debug.LogWarning("currentBulletPool is null.");
                return;
            }

            foreach (Transform projectileTransform in projectileTransforms)
            {
                currentAmmo.Pop(new AmmoData(owner, projectileTransform.position, projectileTransform.up));
            }
        }

        public void Fire()
        {
            TriggerWeapon();
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