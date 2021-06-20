using ANTs.Template;

namespace ANTs.Game
{
    public struct ChangeGunEvent
    {
        public Gun newGun;
        public Gunner holder;

        public ChangeGunEvent(Gun newGun, Gunner holder)
        {
            this.newGun = newGun;
            this.holder = holder;
        }

        static ChangeGunEvent changeGunEvent;
        public static void Trigger(Gunner Holder, Gun newGun)
        {
            changeGunEvent.holder = Holder;
            changeGunEvent.newGun = newGun;
            MMEventManager.TriggerEvent(changeGunEvent);
        }
    }
}