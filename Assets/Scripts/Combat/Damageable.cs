using System;
using UnityEngine;

namespace ANTs.Game
{
    public class Damageable : MonoBehaviour
    {
        public event Action<int> onHealthUpdateEvent;
        public event Action<int> onMaxHealthUpdateEvent;

        [SerializeField] int maxHealth = 100;
        [SerializeField] int health = 100;
        [Space]
        [SerializeField] int defenseByValue = 0;
        [SerializeField] float dodgeChance = 0;

        public int MaxHealth { get => maxHealth; }
        public int Health { 
            get => health; 
            set
            {
                health = Mathf.Clamp(value, 0, MaxHealth);
                onHealthUpdateEvent?.Invoke(health);
            }
        }
        public int DefenseByValue { get => defenseByValue; }
        public float DodgeChance { get => dodgeChance; }

        private void Start()
        {
            onMaxHealthUpdateEvent(maxHealth);
            onHealthUpdateEvent(health);
        }

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