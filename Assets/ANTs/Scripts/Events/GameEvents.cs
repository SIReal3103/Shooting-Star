using ANTs.Template;

namespace ANTs.Core
{
    public struct ChangeGunEvent
    {
        private static ChangeGunEvent changeGunEvent;

        public Gun newGun;
        public GunnerAction holder;

        public ChangeGunEvent(Gun newGun, GunnerAction holder)
        {
            this.newGun = newGun;
            this.holder = holder;
        }

        public static void Trigger(GunnerAction Holder, Gun newGun)
        {
            changeGunEvent.holder = Holder;
            changeGunEvent.newGun = newGun;
            MMEventManager.TriggerEvent(changeGunEvent);
        }
    }
}