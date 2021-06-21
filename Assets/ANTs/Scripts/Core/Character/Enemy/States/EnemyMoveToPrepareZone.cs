using UnityEngine;

using ANTs.Template;

namespace ANTs.Game
{
    public class EnemyMoveToPrepareZone : MonoBehaviour, ISMBCallBack
    {
        [Header("Game system properties")]
        [Tooltip("The animator which control the CallBack")]
        [SerializeField] Animator animator;
        [Tooltip("The object which control the state")]
        [SerializeField] EnemyControlFacade controller;
        [Header("State data")]
        [SerializeField] ANTsPolygon prepareZone;

        private Vector2 preparePosition;

        private void Start()
        {
            SceneLinkedSMB<EnemyMoveToPrepareZone>.Initialise(animator, this);

            preparePosition = prepareZone.GetRandomPointOnSurface();
        }

        public void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller.StartMovingTo(preparePosition);
        }

        public void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}