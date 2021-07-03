using System;
using UnityEngine;

namespace ANTs.Core
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Transform weaponAttachment;
        [SerializeField] bool rotateWithMouse;
        [Space(20)]
        [SerializeField] Weapon initialWeapon;
        [SerializeField] WeaponAmmo initialAmmo;

        private ProjectileWeapon currentProjectileWeapon;
        private MeleeWeapon currentMeleeWeapon;
        private Weapon currentWeapon;

        private void Awake()
        {
            if(initialWeapon)
            {
                currentWeapon = initialWeapon;
                currentWeapon.SetOwner(gameObject);
                if (currentWeapon is ProjectileWeapon)
                {
                    InitProjectileWeapon();
                }                    
                else if(currentWeapon is MeleeWeapon)
                {
                    InitMeleeWeapon();
                }
                else
                {
                    throw new UnityException("Invalid type of current Weapon");
                }

                SetDirection(Vector2.left);
            }
        }

        public void TriggerWeapon()
        {
            currentWeapon.TriggerWeapon();
        }

        public void SetDirection(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            weaponAttachment.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ - 90));
        }

        private void InitMeleeWeapon()
        {
            currentMeleeWeapon = currentWeapon as MeleeWeapon;
        }

        private void InitProjectileWeapon()
        {
            currentProjectileWeapon = currentWeapon as ProjectileWeapon;            
            if(initialAmmo)
            {
                currentProjectileWeapon.SetAmmoPool(initialAmmo.gameObject.GetOrCreatePool());
            }
            else
            {
                currentProjectileWeapon.SetAmmoPool(AmmoManager.Instance.GetDefaultPool());
            }
        }        
    }
}