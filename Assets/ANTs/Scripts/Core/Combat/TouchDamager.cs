using System;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    public class TouchDamager : MonoBehaviour
    {
        public event Action OnHitEvent;

        [HideInInspector]
        public GameObject source;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.transform.CompareTag(source.tag))
            {
                Damageable damageable;
                if (!collision.transform.TryGetComponent(out damageable))
                {
                    return;
                }

                damageable.TakeDamageFrom(GetComponent<Damager>());
                OnHitEvent?.Invoke();
            }
        }
    }
}