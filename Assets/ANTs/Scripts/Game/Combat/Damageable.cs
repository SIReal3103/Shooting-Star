﻿using ANTs.Template;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ANTs.Game
{
    public class Damageable : MonoBehaviour
    {
        [System.Serializable]
        class OnHealthUpdateEvent : UnityEvent<float, float> { }
        public event Action OnHealthReachZeroEvent;

        #region ===============================SERIALIZEFIELD
        [SerializeField] OnHealthUpdateEvent OnHealthUpdate;
        [SerializeField] float maxHealth = 100;
        [SerializeField] float defenseByValue = 0;
        [SerializeField] float dodgeChance = 0;
        #endregion

        private LazyANTs<float> health;

        #region ===============================ACCESSORS
        public float MaxHealth { get => maxHealth; }
        public float Health
        {
            get => health.value;
            set
            {
                health.value = Mathf.Clamp(value, 0, MaxHealth);
                OnHealthUpdate.Invoke(health.value, GetMaxHealth());
                if (Mathf.Approximately(health.value, 0)) OnHealthReachZeroEvent?.Invoke();
            }
        }
        public float DefenseByValue { get => defenseByValue; }
        public float DodgeChance { get => dodgeChance; }
        #endregion

        public float GetMaxHealth() { return GetComponent<BaseStat>().GetStat(StatType.Health); }

        private void Awake()
        {
            health = new LazyANTs<float>(GetMaxHealth);
        }

        private void Start()
        {
            OnHealthUpdate.Invoke(health.value, GetMaxHealth());
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

        public float GetHealthFraction()
        {
           return health.value / GetMaxHealth();
        }
    }
}