using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    public class MeleeWeaponAction : ActionBase
    {
        [SerializeField] AttackArea attackArea;

        private Damager damager;
        [ReadOnly] [SerializeField]
        private GameObject weaponOwner;

        protected override void Awake()
        {
            base.Awake();
            damager = GetComponent<Damager>();
            weaponOwner = GetComponent<MeleeWeaponControl>().GetWeaponOwner();
            attackArea.Source = weaponOwner;
        }

        public override void ActionStart()
        {
            base.ActionStart();
            animatorEvents.OnActorAttackEvent += AttackBehaviour;
        }

        public override void ActionStop()
        {
            base.ActionStop();
            animatorEvents.OnActorAttackEvent -= AttackBehaviour;
        }

        private void AttackBehaviour()
        {
            attackArea.Attack(damager);
        }
    }
}