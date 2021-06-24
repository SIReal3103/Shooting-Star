using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    public class TouchDamager : MonoBehaviour
    {
        /// <summary>
        /// When touch damager hit a dammageble enemy
        /// </summary>
        public event Action OnHitEvent;

        [HideInInspector]
        public GameObject source;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.transform.CompareTag(source.tag))
            {
                if (!collision.transform.TryGetComponent(out Damageable damageable))
                {
                    return;
                }

                damageable.TakeDamageFrom(GetComponent<Damager>());
                OnHitEvent?.Invoke();
            }
        }
    }
}