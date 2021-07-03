using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class WeaponUpgradeHandler
    {
        public static Weapon GetUpgradedWeapon(Weapon weapon)
        {
            ANTsPool pool = weapon.gameObject.GetPool();
            if (weapon is ProjectileWeapon)
            {
                ProjectileWeapon pWeapon = weapon as ProjectileWeapon;
                pool.Pop(new ProjectileWeaponData(
                    pWeapon.GetOwner(),
                    pWeapon.transform.parent,
                    pWeapon.GetCurrentAmmoPool())
                );
                return pWeapon;
            }
            else if (weapon is MeleeWeapon)
            {
                MeleeWeapon mWeapon = weapon as MeleeWeapon;
                pool.Pop(new MeleeWeaponData(
                    mWeapon.GetOwner(),
                    mWeapon.transform.parent)
                );
                return mWeapon;
            }
            else
            {
                throw new UnityException("Can upgrade this item: " + weapon);
            }
        }
    }
}