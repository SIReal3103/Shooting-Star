using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class Projectile : MonoBehaviour
    {
        public Transform Body;
        Rigidbody2D _rigibody2D;
        GameObject _attacker;
        ProjectileFactory _factory;

        public event Action<GameObject> CollidedWithTarget;

        private void Start()
        {
            _rigibody2D = GetComponent<Rigidbody2D>();

            CollidedWithTarget += OnProjectileCollided;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (CollidedWithTarget != null)
                {
                    CollidedWithTarget(collision.gameObject);
                }
            }
        }

        public void Fire(GameObject attacker, Vector3 direction, ProjectileFactory factory)
        {
            this._attacker = attacker;
            _factory = factory;
            SetDirection(direction);
        }

        public void SetDirection(Vector3 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _rigibody2D.AddForce(direction);
        }

        private void OnProjectileCollided(GameObject defender)
        {
            if (_attacker == null || defender == null)
                return;

            Attack attack = _factory.CreateAttack(_attacker, defender);

            var attackables = defender.GetComponentsInChildren(typeof(IAttackable));
            foreach (IAttackable a in attackables)
            {
                a.OnAttack(_attacker, attack);
            }
        }
    }
}