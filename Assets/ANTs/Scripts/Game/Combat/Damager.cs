using UnityEngine;

namespace ANTs.Game
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] float initialDamage = 10;
        [SerializeField] float criticalChance = 1;
        [SerializeField] float critModifier = 0;

        public bool IsEnemy(Damageable damageable)
        {
            return damageable.tag != tag;
        }

        public float GetFinalDamage()
        {
            if (Random.value < criticalChance)
            {
                return (int)(initialDamage * (1f + critModifier));
            }
            return initialDamage;
        }
    }
}