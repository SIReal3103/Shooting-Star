using UnityEngine;

namespace Game.Combat
{
    public class DamageCalculator : MonoBehaviour
    {
        int pureDamage;
        Damageable damageTarget;

        public DamageCalculator(Damageable damageTarget, int pureDamage)
        {
            this.damageTarget = damageTarget;
            this.pureDamage = pureDamage;
        }

        public int GetDamageDealt()
        {
            if (IsDodgeSuccess())
                return Mathf.Min(damageTarget.Health, Mathf.Max(0, pureDamage - damageTarget.DefenseByValue));

            return 0;
        }

        private bool IsDodgeSuccess()
        {
            return Random.value < damageTarget.DodgeChance;
        }
    }
}