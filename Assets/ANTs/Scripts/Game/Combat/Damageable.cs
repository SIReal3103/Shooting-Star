using System;
using UnityEngine;

namespace ANTs.Game
{
    public class Damageable : MonoBehaviour
    {
        public event Action<float> OnHealthUpdateEvent;
        public event Action<float> OnMaxHealthUpdateEvent;
        public event Action OnHealthReachZeroEvent;

        #region ===============================SERIALIZEFIELD
        [SerializeField] float maxHealth = 100;
        [SerializeField] float health = 100;
        [SerializeField] float defenseByValue = 0;
        [SerializeField] float dodgeChance = 0;
        #endregion

        #region ===============================ACCESSORS
        public float MaxHealth { get => maxHealth; }
        public float Health
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
        public float DefenseByValue { get => defenseByValue; }
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
            DrawHealth(calculator.GetCalculatedDamage());
        }

        private void DrawHealth(float health)
        {
            this.Health -= health;
        }
        public void GainHealth(float health)
        {
            this.Health += health;
        }
    }
}