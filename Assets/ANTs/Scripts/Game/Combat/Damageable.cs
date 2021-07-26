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

        public event Action OnActorDieEvent;

        [SerializeField] OnHealthUpdateEvent OnHealthUpdate;
        [SerializeField] OnHealthTakeDamageEvent OnActorTakeDamage;
        [SerializeField] float defenseBonus = 0;
        [SerializeField] float defenseModifier = 0;

        private LazyANTs<float> health;
        private BaseStat baseStat;

        public float Health
        {
            get => health.value;
            private set
            {
                health.value = Mathf.Clamp(value, 0, GetMaxHealth());
                OnHealthUpdate.Invoke(health.value, GetMaxHealth());
            }
        }
        public float GetDefenseBonus() { return defenseBonus; }
        public float GetDefenseModifier() { return defenseModifier; }
        public float GetMaxHealth() { return baseStat.GetStat(StatType.Health); }

        private void Awake()
        {
            health = new LazyANTs<float>(GetMaxHealth);
            baseStat = GetComponent<BaseStat>();
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

            float remainedHealth = DrawHealth(calculatedDamage);
            OnActorTakeDamage.Invoke(calculatedDamage);

            if (Mathf.Approximately(remainedHealth, 0))
            {
                OnActorDieEvent?.Invoke();

                Damager damageCauser = damager.GetDamageCauser();
                if (damageCauser.TryGetComponent(out Experience causerXP))
                {
                    causerXP.GainExperience(baseStat.GetStat(StatType.ExperienceToAward));
                }
            }
        }

        private float DrawHealth(float health)
        {
            this.Health -= health;
            return this.Health;
        }

        public void GainHealth(float health)
        {
            this.Health += health;
        }
    }
}