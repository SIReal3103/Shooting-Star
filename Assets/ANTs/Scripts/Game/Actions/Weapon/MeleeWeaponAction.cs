using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    public class MeleeWeaponAction : ActionBase
    {
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
            GetComponent<IHaveAttackArea>().TriggerAttackArea();
        }
    }
}