using UnityEngine;

namespace ANTs.Core
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] int initialDamage = 10;
        [SerializeField] float criticalChance = 0;
        [SerializeField] float critModifier = 0;

        public bool IsEnemy(Damageable damageable)
        {
            return damageable.tag != tag;
        }

        public int GetFinalDamage()
        {
            if (Random.value < criticalChance)
            {
                return (int)(initialDamage * (1f + critModifier));
            }
            return initialDamage;
        }
    }
}