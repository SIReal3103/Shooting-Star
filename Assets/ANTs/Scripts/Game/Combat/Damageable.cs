using ANTs.Template;
using System;
using UnityEngine;

namespace ANTs.Game
{
    public class Damageable : MonoBehaviour
    {
        public event Action<float> OnHealthFractionUpdateEvent;
        public event Action OnHealthReachZeroEvent;

        #region ===============================SERIALIZEFIELD
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
                if (health.value == 0 && value <= 0) return;
                health.value = Mathf.Clamp(value, 0, MaxHealth);
                OnHealthFractionUpdateEvent?.Invoke(GetHealthFraction());
                if (health.value == 0) OnHealthReachZeroEvent?.Invoke();
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
            OnHealthFractionUpdateEvent?.Invoke(GetHealthFraction());
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

        private float GetHealthFraction()
        {
           return health.value / GetMaxHealth();
        }
    }
}