//using ANTs.Template;
//using UnityEngine;

//namespace ANTs.Core
//{
//    [RequireComponent(typeof(ApproachPlayerTask))]
//    [RequireComponent(typeof(MoverAction))]
//    public class EnemyNinjaAnimatorControl : MonoBehaviour
//    {
//        [SerializeField] Animator animator;

//        private void OnEnable()
//        {
//            GetComponent<ApproachPlayerTask>().OnActorAttackEvent += OnActorAtack;
//        }

//        private void OnDisable()
//        {
//            GetComponent<ApproachPlayerTask>().OnActorAttackEvent -= OnActorAtack;
//        }

//        private void OnActorAtack()
//        {
//            animator.SetTrigger(ANTsGameState.StartAttacking);
//        }
//    }
//}