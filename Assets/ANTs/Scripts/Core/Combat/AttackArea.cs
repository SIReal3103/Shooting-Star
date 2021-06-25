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

        private HashSet<Damageable> damageables;
        private Damager damager;




        private void Awake()
        {
            damager = GetComponent<Damager>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsEnemy(collision))
            {
                if (!collision.transform.TryGetComponent(out Damageable damageable))
                {
                    return;
                }
                damageables.Add(damageable);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (IsEnemy(collision))
            {
                if (!collision.transform.TryGetComponent(out Damageable damageable))
                {
                    return;
                }
                damageables.Remove(damageable);
            }
        }



        private bool IsEnemy(Collision2D collision)
        {
            return !collision.transform.CompareTag(Source.tag);
        }

        public void Attack()
        {
            foreach(Damageable damageable in damageables)
            {
                damageable.TakeDamageFrom(damager);
            }
        }
    }
}