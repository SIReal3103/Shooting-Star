using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    public class AttackArea : MonoBehaviour
    {
        [Tooltip("The source of the attacker, use to compare tag")]
        [SerializeField] GameObject source;

        public GameObject Source { get => source; set => source = value; }

        private HashSet<Damageable> damageables = new HashSet<Damageable>();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (IsEnemy(collider))
            {
                if (collider.transform.TryGetComponent(out Damageable damageable))
                {
                    damageables.Add(damageable);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (IsEnemy(collider))
            {
                if (collider.transform.TryGetComponent(out Damageable damageable))
                {
                    damageables.Remove(damageable);
                }
            }
        }

        private bool IsEnemy(Damageable damageable)
        {
            return !collider.CompareTag(Source.tag);
        }

        public void Attack(Damager damager)
        {
            
            foreach (Damageable damageable in damageables)
            {
                damageable.TakeDamageFrom(damager);
            }
        }
    }
}