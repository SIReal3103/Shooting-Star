using UnityEngine;

namespace ANTs.Core
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [SerializeField] bool rotateWithMouse;
        [Space(20)]
        [SerializeField] Weapon initialWeapon;
        [SerializeField] WeaponAmmo

        private Weapon currentWeapon;


        public void TriggerWeapon()
        {
            currentWeapon.TriggerWeapon();
        }
    }
}