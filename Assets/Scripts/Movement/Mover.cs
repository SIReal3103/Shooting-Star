using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        public float speed = 10f;

        Rigidbody2D rb;

        Vector2 direction;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            direction = Vector2.zero;
        }

        public void MoveWith(Vector2 direction)
        {
            this.direction = direction.normalized;
        }
    }
}