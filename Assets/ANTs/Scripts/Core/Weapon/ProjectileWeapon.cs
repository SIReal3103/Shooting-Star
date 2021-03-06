using ANTs.Template;
using UnityEngine;
using System.Collections;

namespace ANTs.Core
{
    public class ProjectileWeapon : MonoBehaviour, IProgressable
    {
        [Tooltip("The direction which bullet start firing")]
        [SerializeField] Transform[] projectileTransforms;
        [Header("IProgressable")]
        [Space(10)]
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;

        private AmmoPool currentAmmo;
        private GameObject owner;

        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }

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

        public void SetAmmoPool(AmmoPool pool) => currentAmmo = pool;

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
        public AmmoPool ammoPool;

        public ProjectileWeaponData(Transform parent, GameObject owner, AmmoPool ammoPool)
        {
            this.parent = parent;
            this.owner = owner;
            this.ammoPool = ammoPool;
        }
    }
}