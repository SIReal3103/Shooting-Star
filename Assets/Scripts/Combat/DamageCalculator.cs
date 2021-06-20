using UnityEngine;

namespace ANTs.Game
{
    public class DamageCalculator
    {
        private Damager damager;
        private Damageable damageable;

        public DamageCalculator(Damageable damageTarget, Damager damager)
        {
            this.damageable = damageTarget;
            this.damager = damager;
        }

        public int GetDamageDealt()
        {
            if (IsDodgeSuccess()) return 0;
            return FomularResult();
        }

        private int FomularResult()
        {
            return Mathf.Min(damageable.Health, Mathf.Max(0, damager.GetFinalDamage() - damageable.DefenseByValue));
        }

        private bool IsDodgeSuccess()
        {
            return Random.value < damageable.DodgeChance;
        }
    }
}