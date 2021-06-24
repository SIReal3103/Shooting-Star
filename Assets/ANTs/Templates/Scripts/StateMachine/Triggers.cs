using UnityEngine;

namespace ANTs.Template
{
    public static class Triggers
    {
        public static int StartAttacking = Animator.StringToHash("StartAttacking");
        public static int StopAttacking = Animator.StringToHash("StopAttacking");
        public static int StartMoving = Animator.StringToHash("StartMoving");
        public static int StopMoving = Animator.StringToHash("StopMoving");
    }
}