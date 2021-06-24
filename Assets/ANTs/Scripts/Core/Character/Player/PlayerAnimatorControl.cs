using UnityEngine;

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
            animator.SetBool("IsMoving", mover.IsMoving());
        }
    }
}