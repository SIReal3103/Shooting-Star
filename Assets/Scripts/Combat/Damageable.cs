using UnityEngine;

namespace Game.Combat
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] int maxHealth;
        [SerializeField] int health;

        [SerializeField] int defenseByValue;
        [SerializeField] float dodgeChance;

        public int MaxHealth { get => maxHealth; }
        public int Health { get => health; set => health = Mathf.Clamp(value, 0, MaxHealth); }
        public int DefenseByValue { get => defenseByValue; }
        public float DodgeChance { get => dodgeChance; }

        public void DealDamage(int damage)
        {
            DamageCalculator calculator = new DamageCalculator(this, damage);
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
    }
}