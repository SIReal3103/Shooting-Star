using UnityEngine;
using ANTs.Template;

namespace ANTs.Core
{
    public class AttackAction : ActionBase
    {
        [Header("AttackAction")]
        [Space(10)]
        [SerializeField] AttackArea area;

        public void Attack()
        {
            area.Attack();
        }
    }
}