using UnityEngine;

namespace Game.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        Rigidbody2D rb;

        [SerializeField] float speed = 10f;        

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
            rb.MovePosition(rb.position + speed * Time.deltaTime * direction);
            direction = Vector2.zero;
        }

        public void MoveWith(Vector2 direction)
        {
            this.direction = direction.normalized;
        }
    }
}