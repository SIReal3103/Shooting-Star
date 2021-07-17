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
        public event Action OnHealthReachZeroEvent;

        #region ===============================SERIALIZEFIELD
        [SerializeField] OnHealthUpdateEvent OnHealthUpdate;
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