using ANTs.Template;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ANTs.Game
{
    public class Damageable : MonoBehaviour
    {
        [System.Serializable]
        class OnHealthUpdateEvent : UnityEvent<float, float> { }
        [System.Serializable]
        class OnHealthTakeDamageEvent : UnityEvent<float> { }

        public event Action OnHealthReachZeroEvent;

        #region ===============================SERIALIZEFIELD
        [SerializeField] OnHealthUpdateEvent OnHealthUpdate;
        [SerializeField] OnHealthTakeDamageEvent OnHealthTakeDamage;
        [SerializeField] float defenseBonus = 0;
        [SerializeField] float defenseModifier = 0;
        #endregion

        private LazyANTs<float> health;

        #region ===============================ACCESSORS
        public float Health
        {
            get => health.value;
            private set
            {
                health.value = Mathf.Clamp(value, 0, GetMaxHealth());
                OnHealthUpdate.Invoke(health.value, GetMaxHealth());
                if (Mathf.Approximately(health.value, 0)) OnHealthReachZeroEvent?.Invoke();
            }
        }
        public float GetDefenseBonus() { return defenseBonus; }
        public float GetDefenseModifier() { return defenseModifier; }
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
            float calculatedDamage = calculator.GetCalculatedDamage();
            if (Mathf.Approximately(calculatedDamage, 0)) return;

            DrawHealth(calculatedDamage);
            OnHealthTakeDamage.Invoke(calculatedDamage);
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