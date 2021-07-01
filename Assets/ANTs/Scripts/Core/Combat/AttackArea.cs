using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Core
{
    public class AttackArea : MonoBehaviour
    {
        public GameObject Source { get; set; }
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

        private bool IsEnemy(Collider2D collider)
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