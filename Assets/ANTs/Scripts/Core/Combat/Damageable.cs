using System;
using UnityEngine;

namespace ANTs.Core
{
    public class Damageable : MonoBehaviour
    {
        public event Action<int> OnHealthUpdateEvent;
        public event Action<int> OnMaxHealthUpdateEvent;
        public event Action OnHealthReachZeroEvent;

        #region ===============================SERIALIZEFIELD
        [SerializeField] int maxHealth = 100;
        [SerializeField] int health = 100;
        [Space]
        [SerializeField] int defenseByValue = 0;
        [SerializeField] float dodgeChance = 0;
        #endregion

        #region ===============================ACCESSORS
        public int MaxHealth { get => maxHealth; }
        public int Health
        {
            get => health;
            set
            {
                if (health == 0 && value <= 0) return;
                health = Mathf.Clamp(value, 0, MaxHealth);
                OnHealthUpdateEvent?.Invoke(health);
                if (health == 0) OnHealthReachZeroEvent?.Invoke();
            }
        }
        public int DefenseByValue { get => defenseByValue; }
        public float DodgeChance { get => dodgeChance; }
        #endregion

        private void Start()
        {
            OnMaxHealthUpdateEvent?.Invoke(maxHealth);
            OnHealthUpdateEvent?.Invoke(health);
        }

        public void TakeDamageFrom(Damager damager)
        {
            DamageCalculator calculator = new DamageCalculator(this, damager);
            DrawHealth(calculator.GetDamageDealt());
        }

        private void DrawHealth(int health)
        {
            this.Health -= health;
        }
        public void GainHealth(int health)
        {
            this.Health += health;
        }
    }
}