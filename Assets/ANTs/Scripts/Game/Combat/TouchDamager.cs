using System;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(Damager))]
    public class TouchDamager : MonoBehaviour
    {
        /// <summary>
        /// When touch damager hit a dammageble enemy
        /// </summary>
        public event Action OnHitEvent;

        [Tooltip("The source of the attacker, use to compare tag")]
        [SerializeField] GameObject source;
        [Tooltip("If the enemy stay in the collider, how long between dammages occur")]
        [SerializeField] float timeBetweenHits = 0.5f;

        public GameObject Source { get => source; set => source = value; }

        private float timeSinceLastHit;

        private void OnEnable()
        {
            timeSinceLastHit = Mathf.Infinity;
        }

        private void Update()
        {
            timeSinceLastHit += Time.deltaTime;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.enabled) return;

            if (IsEnemy(collision))
            {
                if (collision.transform.TryGetComponent(out Damageable damageable))
                {
                    Attack(damageable);
                }
            }
        }

        private bool IsEnemy(Collision2D collision)
        {
            return !collision.transform.CompareTag(Source.tag);
        }

        private void Attack(Damageable damageable)
        {
            if (timeSinceLastHit < timeBetweenHits) return;
            timeSinceLastHit = 0;

            damageable.TakeDamageFrom(GetComponent<Damager>());
            OnHitEvent?.Invoke();
        }
    }
}