using UnityEngine;
using Game.Combat;
using System;

namespace Game.Core
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Damager))]

    public class Bullet : Projectile
    {
        [SerializeField]
        bool destroyWhenOutOfScreen = false;
        [SerializeField]
        Vector2 outScreenOffSet = Vector2.zero;
        
        public BulletObject bulletObject;

        new void Update()
        {
            base.Update();

            if(destroyWhenOutOfScreen && CheckIsOutOfScreen())
            {
                ReturnToPool();
            }
        }

        private bool CheckIsOutOfScreen()
        {
            Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
            Debug.Log(screenPosition);
            return
                screenPosition.x < -outScreenOffSet.x || screenPosition.x > 1f + outScreenOffSet.x ||
                screenPosition.y < -outScreenOffSet.y || screenPosition.y > 1f + outScreenOffSet.y;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(!collision.transform.CompareTag(source.tag))
            {
                Damageable damageable;

                if (!collision.transform.TryGetComponent<Damageable>(out damageable))
                {
                    Debug.Log("Tag is not comparable with object components");
                }

                GetComponent<Damager>().DealtDamageTo(damageable);
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            bulletObject.ReturnToPool();
        }
    }
}