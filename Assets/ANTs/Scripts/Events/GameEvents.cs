using ANTs.Template;

namespace ANTs.Game
{
    public struct ChangeGunEvent
    {
        private static ChangeGunEvent changeGunEvent;

        public Gun newGun;
        public Gunner holder;

        public ChangeGunEvent(Gun newGun, Gunner holder)
        {
            this.newGun = newGun;
            this.holder = holder;
        }

        public static void Trigger(Gunner Holder, Gun newGun)
        {
            changeGunEvent.holder = Holder;
            changeGunEvent.newGun = newGun;
            MMEventManager.TriggerEvent(changeGunEvent);
        }
    }
}