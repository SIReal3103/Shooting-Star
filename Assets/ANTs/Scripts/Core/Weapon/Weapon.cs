using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public enum WeaponType
    {
        ProjectileWeapon,
        MeleeWeapon
    }

    public abstract class Weapon : MonoBehaviour, IProgressable
    {
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;

        protected GameObject owner;

        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }
        public GameObject GetOwner() { return owner; }
        public virtual void SetOwner(GameObject owner) { this.owner = owner; }

        public abstract void TriggerWeapon();
        public abstract WeaponType GetWeaponType();
    }

    public abstract class WeaponData
    {
        public GameObject owner;
        public Transform parent;

        public WeaponData(GameObject owner, Transform parent)
        {
            this.owner = owner;
            this.parent = parent;
        }
    }
}