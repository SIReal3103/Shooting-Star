using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(Damager))]
    public class ProjectileWeapon : Weapon, IPoolable
    {
        [Tooltip("The direction which bullets start firing")]
        [SerializeField] Transform[] projectileTransforms;

        private ANTsPool ammoPool;
        private Damager damager;

        public ANTsPool GetAmmoPool() { return ammoPool; }
        public void SetAmmoPool(ANTsPool pool) { ammoPool = pool; }

        private void Awake()
        {
            damager = GetComponent<Damager>();
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
                Debug.LogWarning("There are no avaiable ammo for " + this);
                return;
            }

            foreach (Transform projectileTransform in projectileTransforms)
            {
                ammoPool.Pop(new AmmoData(owner, this, projectileTransform.position, projectileTransform.up));
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

        public void WakeUp(object param)
        {
            Init((ProjectileWeaponData)param);
        }

        public void Sleep() { }
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