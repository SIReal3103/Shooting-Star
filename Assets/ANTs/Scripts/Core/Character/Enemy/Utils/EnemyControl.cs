using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(MeleeAttackAction))]
    [RequireComponent(typeof(DieAction))]
    public class EnemyControl : MonoBehaviour
    {
        private MeleeWeaponControl meleeWeapon;

        private void Awake()
        {
            meleeWeapon = GetComponent<MeleeAttackAction>().GetMeleeWeapon();
        }

        private void OnEnable()
        {
            GetComponent<Damageable>().OnHealthReachZeroEvent += DeadBehaviour;
        }

        private void OnDisable()
        {
            GetComponent<Damageable>().OnHealthReachZeroEvent -= DeadBehaviour;
        }

        private void DeadBehaviour()
        {
            GetComponent<DieAction>().ActionStart();
            meleeWeapon.OwnerDie();
        }
    }
}

