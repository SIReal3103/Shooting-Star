using UnityEngine;

namespace ANTs.Game
{
    public class DamageCalculator
    {
        private Damager damager;
        private Damageable damageable;

        public DamageCalculator(Damageable damageable, Damager damager)
        {
            this.damageable = damageable;
            this.damager = damager;
        }

        public float GetCalculatedDamage()
        {
            return Mathf.Min(damageable.Health,
                 Mathf.Max(0, (damager.GetFinalDamage() - damageable.GetDefenseBonus())) * 
                (1 - damageable.GetDefenseModifier())
            );
        }
    }
}