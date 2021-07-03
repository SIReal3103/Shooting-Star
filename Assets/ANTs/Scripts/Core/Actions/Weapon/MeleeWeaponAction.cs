using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(Weapon))]
    public class MeleeWeaponAction : ActionBase
    {
        [SerializeField] AttackArea attackArea;

        private Damager damager;

        protected override void Awake()
        {
            base.Awake();
            damager = GetComponent<Damager>();
            attackArea.SetSource(GetComponent<Weapon>().GetWeaponOwner());
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