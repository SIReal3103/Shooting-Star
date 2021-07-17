using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{    
    [RequireComponent(typeof(MeleeWeaponAction))]
    public class MeleeWeapon : Weapon, IPoolable, IHaveAttackArea
    {
        [SerializeField] AttackArea attackArea;

        public override void Init(WeaponData data)
        {
            base.Init(data);
            MeleeWeaponData mData = (MeleeWeaponData)data; // to protect type
            attackArea.SetSource(owner);
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

        public void WakeUp(object param)
        {
            Init((MeleeWeaponData)param);
        }

        public void Sleep() { }
    }

    public class MeleeWeaponData : WeaponData
    {
        public MeleeWeaponData(GameObject owner, Transform parent)
            : base(owner, parent)
        {

        }
    }
}