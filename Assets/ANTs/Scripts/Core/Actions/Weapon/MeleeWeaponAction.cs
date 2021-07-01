using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    public class MeleeWeaponAction : ActionBase
    {
        [SerializeField] GameObject weaponOwner;
        [SerializeField] AttackArea attackArea;

        private Damager damager;

        protected override void Awake()
        {
            base.Awake();
            damager = GetComponent<Damager>();
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