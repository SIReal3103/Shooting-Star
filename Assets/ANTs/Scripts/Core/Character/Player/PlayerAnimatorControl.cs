using UnityEngine;
using ANTs.Template;

namespace ANTs.Core
{
    [RequireComponent(typeof(MoverAction))]
    public class PlayerAnimatorControl : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private MoverAction mover;

        private void Awake()
        {
            mover = GetComponent<MoverAction>();
        }

        private void Update()
        {
            animator.SetBool(ANTsGameState.IsMoving, mover.IsMoving());
        }
    }
}