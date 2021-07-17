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
            return GetFinalDamage();
        }

        private float GetFinalDamage()
        {
            return Mathf.Min(damageable.Health, Mathf.Max(0, damager.CalculateFindalDamage() - damageable.DefenseByValue));
        }
    }
}