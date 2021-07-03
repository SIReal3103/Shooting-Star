﻿using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class ProjectileWeapon : Weapon, IProgressable
    {
        [Tooltip("The direction which bullet start firing")]
        [SerializeField] Transform[] projectileTransforms;

        private ANTsPool currentAmmo;

        #region ==================================Behaviours
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

        public void SetAmmoPool(ANTsPool pool) => currentAmmo = pool;

        public void Fire()
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
        #endregion


        public void ReturnToPool()
        {
            gameObject.ReturnToPoolOrDestroy();
        }

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

    public class ProjectileWeaponData
    {
        public Transform parent;
        public GameObject owner;
        public ANTsPool ammoPool;

        public ProjectileWeaponData(Transform parent, GameObject owner, ANTsPool ammoPool)
        {
            this.parent = parent;
            this.owner = owner;
            this.ammoPool = ammoPool;
        }
    }
}