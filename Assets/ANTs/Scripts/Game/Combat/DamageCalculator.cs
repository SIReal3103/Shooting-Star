using UnityEngine;

namespace ANTs.Game
{
    public class DamageCalculator
    {
        private Damager attacker;
        private Damageable defender;

        public DamageCalculator(Damageable defender, Damager attacker)
        {
            this.defender = defender;
            this.attacker = attacker;
        }

        public float GetCalculatedDamage()
        {
            return Mathf.Min(defender.Health,
                 Mathf.Max(0, (GetFinalDamage(attacker) - defender.GetDefenseBonus())) *
                (1 - defender.GetDefenseModifier())
            );
        }

        private float GetFinalDamage(Damager damager)
        {
            return damager.GetDamageData().damageBonus * (1f + damager.GetDamageData().damageModifier);
        }
    }
}