using UnityEngine;

namespace Game.Combat
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] int initialDamage = 10;
        [SerializeField] float criticalChance = 0;
        [SerializeField] float critEfficent = 0;

        public int GetFinalDamage()
        {
            if(Random.value < criticalChance)
            {
                return (int) (initialDamage * (1f + criticalChance));
            }

            return initialDamage;
        }
    }
}