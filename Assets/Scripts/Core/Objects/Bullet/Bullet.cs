using UnityEngine;
using Game.Combat;
using System;

namespace Game.Core
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Damager))]

    public class Bullet : Projectile, IANTsPoolObject<BulletPool, Bullet>
    {
        [SerializeField]
        bool destroyWhenOutOfScreen = true;
        [SerializeField]
        Vector2 outScreenOffSet = Vector2.zero;

        public BulletPool CurrentPool { get; set; }

        private void Update()
        {
            if(destroyWhenOutOfScreen && CheckIsOutOfScreen())
            {
                ReturnToPool();
            }
        }

        private bool CheckIsOutOfScreen()
        {
            Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
            return
                screenPosition.x < -outScreenOffSet.x || screenPosition.x > 1f + outScreenOffSet.x ||
                screenPosition.y < -outScreenOffSet.y || screenPosition.y > 1f + outScreenOffSet.y;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(!collision.transform.CompareTag(source.tag))
            {
                Damageable damageable;
                if (!collision.transform.TryGetComponent(out damageable))
                {
                    return;
                }

                damageable.TakeDamageFrom(GetComponent<Damager>());
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            CurrentPool.ReturnToPool(this);
        }

        public void WakeUp(System.Object args)
        {
            gameObject.SetActive(true);

            BulletData data = args as BulletData;

            transform.position = data.origin;
            SetDirection(data.moveDirection);
            source = data.source;
        }

        public void Sleep()
        {
            gameObject.SetActive(false);
        }

    }
}