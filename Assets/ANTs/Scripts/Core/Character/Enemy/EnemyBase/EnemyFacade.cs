using UnityEngine;

namespace ANTs.Core
{
    //TODO: Enemy not die
    [RequireComponent(typeof(Damageable))]
    public class EnemyFacade : MonoBehaviour
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

