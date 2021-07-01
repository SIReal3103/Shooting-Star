using ANTs.Template;

namespace ANTs.Core
{
    public struct ChangeGunEvent
    {
        private static ChangeGunEvent changeGunEvent;

        public ProjectileWeaponControl newGun;
        public ShootAction holder;

        public ChangeGunEvent(ProjectileWeaponControl newGun, ShootAction holder)
        {
            this.newGun = newGun;
            this.holder = holder;
        }

        public static void Trigger(ShootAction Holder, ProjectileWeaponControl newGun)
        {
            changeGunEvent.holder = Holder;
            changeGunEvent.newGun = newGun;
            MMEventManager.TriggerEvent(changeGunEvent);
        }
    }
}