using UnityEngine;

namespace ANTs.Core
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [SerializeField] bool rotateWithMouse;
        [Space(20)]
        [SerializeField] MeleeWeapon meleeWeapon;        
        [SerializeField] ProjectileWeapon projectileWeapon;
        [SerializeField] WeaponAmmo weaponAmmo;
        [SerializeField] Weapon CurrentWeapon;
    }
}