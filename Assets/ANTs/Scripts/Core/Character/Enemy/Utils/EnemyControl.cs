using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(MeleeAttackAction))]
    [RequireComponent(typeof(DieAction))]
    public class EnemyControl : MonoBehaviour
    {
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
            GetComponent<WeaponHandler>().currentMeleeWeapon.OwnerDie();
        }
    }
}

