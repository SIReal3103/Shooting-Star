using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class WeaponUpgradeHandler
    {
        public static void UpgradeWeapon(ref Weapon weapon)
        {
            Weapon old = weapon;
            ANTsPool pool = weapon.gameObject.GetPool();
            if (weapon is ProjectileWeapon)
            {
                ProjectileWeapon pWeapon = weapon as ProjectileWeapon;
                ProjectileWeaponManager.Instance.ProgressNextPool(ref pool);
                pool.Pop(new ProjectileWeaponData(
                    pWeapon.GetOwner(),
                    pWeapon.transform.parent,
                    pWeapon.GetCurrentAmmoPool())
                );
                weapon = pWeapon;
            }
            else if (weapon is MeleeWeapon)
            {
                MeleeWeapon mWeapon = weapon as MeleeWeapon;
                MeleeWeaponManager.Instance.ProgressNextPool(ref pool);
                pool.Pop(new MeleeWeaponData(
                    mWeapon.GetOwner(),
                    mWeapon.transform.parent)
                );
                weapon = mWeapon;
            }
            else
            {
                throw new UnityException("Can upgrade this item: " + weapon);
            }
            old.gameObject.ReturnToPoolOrDestroy();
        }

        public static void UpgradeWeaponAmmo(ProjectileWeapon pWeapon, ref ANTsPool ammo)
        {
            AmmoManager.Instance.ProgressNextPool(ref ammo);
            pWeapon.SetAmmoPool(ammo);
        }
    }
}