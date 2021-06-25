using UnityEngine;

using ANTs.Template;

namespace ANTs.Core
{
    //TODO: Enemy not die
    [RequireComponent(typeof(Damageable))]
    public class EnemyFacade : MonoBehaviour
    {
        private Damageable damageable;
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        private void Update()
        {
            if(damageable.IsDead())
            {
            }
        }
    }
}

