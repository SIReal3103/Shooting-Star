using System;
using ANTs.Game;
using UnityEngine;

namespace ANTs.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        static private string HEALTH_BAR_PATH = "Bar";

        [SerializeField] Damageable observer;
        
        private int currentHealth;
        private int maxHealth;

        private Transform healthBar;

        private void OnEnable()
        {
            observer.onHealthUpdateEvent += OnHealthUpdate;
            observer.onMaxHealthUpdateEvent += OnMaxHealthUpdate;
        }

        private void OnDisable()
        {
            observer.onHealthUpdateEvent -= OnHealthUpdate;
            observer.onMaxHealthUpdateEvent -= OnMaxHealthUpdate;
        }

        private void Start()
        {
            healthBar = transform.Find(HEALTH_BAR_PATH);
        }

        private void OnHealthUpdate(int health)
        {
            UpdateHealth(health);
        }

        private void OnMaxHealthUpdate(int health)
        {
            UpdateMaxHealth(health);
        }

        public void UpdateMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            UpdateHealthBar();
        }

        public void UpdateHealth(int currentHealth)
        {
            this.currentHealth = currentHealth;
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if (maxHealth == 0) return;
            healthBar.localScale = new Vector3((float)currentHealth / maxHealth, 1);
        }
    }
}