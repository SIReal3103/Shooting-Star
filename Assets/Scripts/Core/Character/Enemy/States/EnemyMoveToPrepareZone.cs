using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    public class EnemyMoveToPrepareZone : SceneLinkedSMB<EnemyControlFacade>
    {
        [SerializeField] ANTsPolygon prepareZone;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.StartMovingTo(prepareZone.GetRandomPointOnSurface());
        }
    }
}