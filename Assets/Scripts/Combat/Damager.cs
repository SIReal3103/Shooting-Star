using UnityEngine;

namespace Game.Combat
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] float criticalChance;
        [SerializeField] float critEfficent;
        [SerializeField] int initialDamage;

        public void DealtDamageTo(Damageable damageable)
        {
            damageable.DealDamage(GetDamageDealt());
        }

        private int GetDamageDealt()
        {
            if(Random.value < criticalChance)
            {
                return (int) (initialDamage * (1f + criticalChance));
            }

            return 0;
        }
    }
}