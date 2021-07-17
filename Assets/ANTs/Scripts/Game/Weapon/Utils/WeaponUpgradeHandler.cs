using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class WeaponUpgradeHandler
    {
        public static void UpgradeWeapon(ref Weapon weapon)
        {
            Weapon old = weapon;
            ANTsPool weaponPool = weapon.gameObject.GetPool();
            if (weapon is ProjectileWeapon)
            {
                ProjectileWeapon pWeapon = (ProjectileWeapon)weapon;
                ProjectileWeaponManager.Instance.ProgressNextPool(ref weaponPool);
                weapon = weaponPool.Pop(new ProjectileWeaponData(
                    pWeapon.owner,
                    pWeapon.transform.parent,
                    pWeapon.GetAmmoPool())
                ).GetComponent<Weapon>();
            }
            else if (weapon is MeleeWeapon)
            {
                MeleeWeapon mWeapon = (MeleeWeapon)weapon;
                MeleeWeaponManager.Instance.ProgressNextPool(ref weaponPool);
                weapon = weaponPool.Pop(new MeleeWeaponData(
                    mWeapon.owner,
                    mWeapon.transform.parent)
                ).GetComponent<Weapon>();
            }
            else
            {
                throw new UnityException("Can't upgrade this item: " + weapon);
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