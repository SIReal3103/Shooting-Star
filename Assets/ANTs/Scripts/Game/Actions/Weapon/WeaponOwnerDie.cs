using ANTs.Template;

namespace ANTs.Core
{
    public class WeaponOwnerDie : ActionBase
    {
        public override void ActionStart()
        {
            base.ActionStart();
            gameObject.ReturnToPoolOrDestroy();
        }
    }
}