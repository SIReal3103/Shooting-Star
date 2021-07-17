using ANTs.Template;

namespace ANTs.Core
{
    public class MeleeAttackAction : ActionBase
    {
        public override void ActionStart()
        {
            base.ActionStart();
            GetComponent<WeaponHandler>().TriggerMeleeWeapon();
        }
    }
}