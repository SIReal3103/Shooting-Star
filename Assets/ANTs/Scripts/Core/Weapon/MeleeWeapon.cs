using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(MeleeWeaponAction))]
    public class MeleeWeapon : Weapon
    {
        [SerializeField] AttackArea attackArea;
        public AttackArea GetAttackArea() { return attackArea; }

        private void Awake()
        {
            gameObject.SetWakeUpDelegate(args => {
                Init((MeleeWeaponData)args);
            });
        }


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
    }

    public class MeleeWeaponData : WeaponData
    {
        public MeleeWeaponData(GameObject owner, Transform parent)
            : base(owner, parent)
        {

        }
    }
}