using ANTs.Template;

namespace ANTs.Game
{
    public struct ChangeGunEvent
    {
        private static ChangeGunEvent changeGunEvent;

        public ProjectileWeapon newGun;
        public ShootAction holder;

        public ChangeGunEvent(ProjectileWeapon newGun, ShootAction holder)
        {
            this.newGun = newGun;
            this.holder = holder;
        }

        public static void Trigger(ShootAction Holder, ProjectileWeapon newGun)
        {
            changeGunEvent.holder = Holder;
            changeGunEvent.newGun = newGun;
            MMEventManager.TriggerEvent(changeGunEvent);
        }
    }
}