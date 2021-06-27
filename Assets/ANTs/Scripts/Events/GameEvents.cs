using ANTs.Template;

namespace ANTs.Core
{
    public struct ChangeGunEvent
    {
        private static ChangeGunEvent changeGunEvent;

        public Gun newGun;
        public ShootAction holder;

        public ChangeGunEvent(Gun newGun, ShootAction holder)
        {
            this.newGun = newGun;
            this.holder = holder;
        }

        public static void Trigger(ShootAction Holder, Gun newGun)
        {
            changeGunEvent.holder = Holder;
            changeGunEvent.newGun = newGun;
            MMEventManager.TriggerEvent(changeGunEvent);
        }
    }
}