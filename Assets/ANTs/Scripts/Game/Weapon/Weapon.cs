using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(Damager))]
    public abstract class Weapon : MonoBehaviour, IProgressable
    {
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;
        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }

        [ReadOnly]
        public GameObject owner;

        public virtual void Init(WeaponData data)
        {
            transform.SetParentPreserve(data.parent);
            this.owner = data.owner;
            if (owner.TryGetComponent(out Damager weaponOwner))
            {
                GetComponent<Damager>().CombineDamageData(weaponOwner.GetDamageData(), weaponOwner);
            }
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