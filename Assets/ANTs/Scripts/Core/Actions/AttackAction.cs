using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class AttackAction : ActionBase
    {
        [Header("AttackAction")]
        [Space(10)]
        [SerializeField] AttackArea area;

        public override void ActionStart()
        {
            base.ActionStart();
            animatorEvents.OnActorAttackEvent += area.Attack;
        }

        public override void ActionStop()
        {
            base.ActionStop();
            animatorEvents.OnActorAttackEvent -= area.Attack;
        }
    }
}