using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class MeleeAttackAction : ActionBase
    {
        [Header("AttackAction")]
        [Space(10)]
        [SerializeField] AttackArea area;
        [SerializeField] MeleeWeaponControl meleeWeapon;

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
            area.Attack();
        }
    }
}