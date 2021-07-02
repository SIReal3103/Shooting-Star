using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MeleeWeaponAction))]
    public class MeleeWeaponControl : MonoBehaviour
    {
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