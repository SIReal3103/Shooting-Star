using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class MeleeAttackAction : ActionBase
    {
        [SerializeField] MeleeWeaponControl meleeWeapon;

        public MeleeWeaponControl GetMeleeWeapon()
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