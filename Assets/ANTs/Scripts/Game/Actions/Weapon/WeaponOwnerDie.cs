using ANTs.Template;

namespace ANTs.Game
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