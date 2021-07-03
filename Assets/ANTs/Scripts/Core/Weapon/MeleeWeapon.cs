using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(MeleeWeaponAction))]
    public class MeleeWeapon : Weapon
    {
        [SerializeField] AttackArea attackArea;

        public AttackArea GetAttackArea() { return attackArea; }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            attackArea.SetSource(owner);
        }

        private void Awake()
        {
            SetOwner(owner);
        }

        public void OwnerDie()
        {
            GetComponent<MeleeOwnerDieAction>().ActionStart();
        }

        public void Attack()
        {
            TriggerWeapon();            
        }

        public override void TriggerWeapon()
        {
            GetComponent<MeleeWeaponAction>().ActionStart();
        }

        public void TriggerAttackArea()
        {
            attackArea.Attack(GetComponent<Damager>());
        }
    }


    public class MeleeWeaponData : WeaponData
    {
        public MeleeWeaponData(GameObject owner, Transform parent)
            : base(owner, parent)
        {

        }
    }
}