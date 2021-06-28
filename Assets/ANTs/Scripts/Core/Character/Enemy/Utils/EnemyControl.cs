using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damageable))]
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
        }
    }
}

