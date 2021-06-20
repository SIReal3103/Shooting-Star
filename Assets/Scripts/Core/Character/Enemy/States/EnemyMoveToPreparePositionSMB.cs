using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    public class EnemyMoveToPreparePositionSMB : SceneLinkedSMB<EnemyBehaviour>
    {
        [SerializeField] ANTsPolygon prepareZone;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //TODO: this
        }
    }
}