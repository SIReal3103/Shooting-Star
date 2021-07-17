using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    public abstract class Weapon : MonoBehaviour, IProgressable
    {
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;

        [ReadOnly]
        public GameObject owner;

        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }

        public virtual void Init(WeaponData data)
        {
            transform.SetParentPreserve(data.parent);
            this.owner = data.owner;
        }

        public abstract void TriggerWeapon();


        public void OwnerDie()
        {
            GetComponent<WeaponOwnerDie>().ActionStart();
        }
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