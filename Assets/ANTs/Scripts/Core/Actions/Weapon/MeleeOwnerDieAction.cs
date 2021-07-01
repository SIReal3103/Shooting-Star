using ANTs.Template;

namespace ANTs.Core
{
    public class MeleeOwnerDieAction : ActionBase
    {
        public override void ActionStart()
        {
            base.ActionStart();
            gameObject.ReturnToPoolOrDestroy();
        }
    }
}