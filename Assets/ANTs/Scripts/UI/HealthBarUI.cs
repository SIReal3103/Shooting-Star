using System;
using ANTs.Core;
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

        private void Awake()
        {
            healthBar = transform.Find(HEALTH_BAR_PATH);
        }

        private void OnEnable()
        {
            observer.OnHealthUpdateEvent += OnHealthUpdate;
            observer.OnMaxHealthUpdateEvent += OnMaxHealthUpdate;
        }

        private void OnDisable()
        {
            observer.OnHealthUpdateEvent -= OnHealthUpdate;
            observer.OnMaxHealthUpdateEvent -= OnMaxHealthUpdate;
        }

        private void OnHealthUpdate(int currentHealth)
        {
            this.currentHealth = currentHealth;
            UpdateHealthBar();
        }

        private void OnMaxHealthUpdate(int maxHealth)
        {
            this.maxHealth = maxHealth;
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if (maxHealth == 0) return;
            healthBar.localScale = new Vector3((float)currentHealth / maxHealth, 1);
        }
    }
}