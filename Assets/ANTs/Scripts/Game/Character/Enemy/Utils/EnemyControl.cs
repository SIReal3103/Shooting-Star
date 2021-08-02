using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(DieAction))]
    public class EnemyControl : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Damageable>().OnActorDieEvent += DeadBehaviour;
        }

        private void OnDisable()
        {
            GetComponent<Damageable>().OnActorDieEvent -= DeadBehaviour;
        }

        private void DeadBehaviour()
        {
            GetComponent<DieAction>().ActionStart();
            if (TryGetComponent(out WeaponHandler weaponHandler))
            {
                weaponHandler.NotifyWeaponOwnerDie();
            }
        }
    }
}

