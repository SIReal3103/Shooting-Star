using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ANTs.Template;

namespace ANTs.Core
{
    public class MeleeWeaponAction : ActionBase
    {
        [SerializeField] AttackArea area;
        [SerializeField] AnimatorEvents events;

        public override void ActionStart()
        {
            base.ActionStart();
            events.OnActorAttackEvent += area.Attack;
        }

        public override void ActionStop()
        {
            base.ActionStop();
            events.OnActorAttackEvent -= area.Attack;
        }
    }
}