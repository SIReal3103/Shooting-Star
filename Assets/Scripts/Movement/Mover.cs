using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Combat;

namespace Game.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        public float speed = 10f;

        Rigidbody2D rb;

        // Fire bulllet
        [SerializeField] KeyCode FireBulletKey = KeyCode.Space;
        [SerializeField] ProjectileFactory ProjectileFactory;

        Vector2 direction;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            direction = Vector2.zero;
     
            if (Input.GetKeyDown(FireBulletKey))
            {
                ProjectileFactory.Fire(this.gameObject, Vector3.up);
            }
        }

        public void MoveWith(Vector2 direction)
        {
            this.direction = direction.normalized;
        }
    }
}