using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MeleeWeaponAction))]
    public class MeleeWeaponControl : MonoBehaviour
    {
        public void Attack()
        {
            GetComponent<MeleeWeaponAction>().ActionStart();
        }
    }
}