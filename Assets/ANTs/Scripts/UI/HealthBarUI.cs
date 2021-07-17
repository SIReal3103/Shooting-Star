using ANTs.Game;
using UnityEngine;

namespace ANTs.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] Damageable observer;
        [SerializeField] Transform healthBarTransform;

        private void OnEnable()
        {
            observer.OnHealthFractionUpdateEvent += OnHealthUpdate;
        }

        private void OnDisable()
        {
            observer.OnHealthFractionUpdateEvent -= OnHealthUpdate;
        }

        private void OnHealthUpdate(float healthFraction)
        {
            healthBarTransform.localScale = new Vector3(healthFraction, 1);
        }
    }
}