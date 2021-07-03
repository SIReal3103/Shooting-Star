using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class MeleeAttackAction : ActionBase
    {
        [SerializeField] MeleeWeapon meleeWeapon;

        public MeleeWeapon GetMeleeWeapon()
        {
            return meleeWeapon;
        }

        public override void ActionStart()
        {
            base.ActionStart();
            meleeWeapon.Attack();
        }
    }
}