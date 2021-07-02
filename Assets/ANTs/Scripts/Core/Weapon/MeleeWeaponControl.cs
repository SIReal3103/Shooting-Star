using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MeleeWeaponAction))]
    public class MeleeWeaponControl : MonoBehaviour
    {
        [SerializeField] GameObject weaponOwner;

        public GameObject GetWeaponOwner() { return weaponOwner; }

        public void OwnerDie()
        {
            GetComponent<MeleeOwnerDieAction>().ActionStart();
        }

        public void Attack()
        {
            GetComponent<MeleeWeaponAction>().ActionStart();
        }
    }
}