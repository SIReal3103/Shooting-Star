using UnityEngine;

namespace ANTs.Template
{
    public static class ANTsGameState
    {
        public static int StartAttacking = Animator.StringToHash("StartAttacking");
        public static int StopAttacking = Animator.StringToHash("StopAttacking");
        public static int StartMoving = Animator.StringToHash("StartMoving");
        public static int StopMoving = Animator.StringToHash("StopMoving");
        public static int StartDying = Animator.StringToHash("StartDying");

        public static int IsMoving = Animator.StringToHash("IsMoving");
    }
}