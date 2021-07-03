using UnityEngine;

namespace ANTs.Core
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [SerializeField] ProjectileWeapon projWeapon;
        [SerializeField] MeleeWeapon meleeWeapon;
    }
}