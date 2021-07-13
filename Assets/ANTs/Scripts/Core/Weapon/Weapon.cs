using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public abstract class Weapon : MonoBehaviour, IProgressable
    {
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;

        public GameObject owner;

        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }

        public virtual void Init(WeaponData data)
        {
            transform.SetParent(data.parent);
            this.owner = data.owner;
        }

        public abstract void TriggerWeapon();
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