using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    Vector2 direction;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        direction = Vector2.zero;
    }

    public void MoveTo(Vector2 direction)
    {
        this.direction = direction.normalized;
    }
}
