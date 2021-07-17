using ANTs.Template;

namespace ANTs.Game
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