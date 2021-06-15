using UnityEngine;

namespace Game.Combat
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;
        [SerializeField] int health = 100;

        [SerializeField] int defenseByValue = 0;
        [SerializeField] float dodgeChance = 0;

        public int MaxHealth { get => maxHealth; }
        public int Health { get => health; set => health = Mathf.Clamp(value, 0, MaxHealth); }
        public int DefenseByValue { get => defenseByValue; }
        public float DodgeChance { get => dodgeChance; }

        public void TakeDamageFrom(Damager damager)
        {
            DamageCalculator calculator = new DamageCalculator(this, damager);
            DrawHealth(calculator.GetDamageDealt());
        }

        public void DrawHealth(int health)
        {
            this.Health -= health;
        }

        public void GainHealth(int health)
        {
            this.Health += health;
        }

        public bool IsDead()
        {
            return this.Health == 0;
        }
    }
}