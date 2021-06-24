using UnityEngine;
using ANTs.Template;

namespace ANTs.Core
{
    [RequireComponent(typeof(Mover))]
    public class PlayerAnimatorControl : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            animator.SetBool(ANTsTransition.IsMoving, mover.IsMoving());
        }
    }
}