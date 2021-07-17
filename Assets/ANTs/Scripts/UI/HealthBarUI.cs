using ANTs.Game;
using UnityEngine;

namespace ANTs.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        static private string HEALTH_BAR_PATH = "Bar";

        [SerializeField] Damageable observer;

        private float currentHealth;
        private float maxHealth;

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

        private void OnHealthUpdate(float currentHealth)
        {
            this.currentHealth = currentHealth;
            UpdateHealthBar();
        }

        private void OnMaxHealthUpdate(float maxHealth)
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