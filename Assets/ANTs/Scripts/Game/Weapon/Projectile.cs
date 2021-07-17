using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(MoveAction))]
    public abstract class Projectile : MonoBehaviour
    {

        [Tooltip("Decide whether if the bullet will be destroyed when out of player view")]
        [SerializeField] bool destroyWhenOutOfScreen = true;
        [Tooltip("The boundaries of which the bullet will be destroyed")]
        [SerializeField] Vector2 outScreenOffSet = Vector2.zero;

        private MoveAction mover;

        protected virtual void Awake()
        {
            mover = GetComponent<MoveAction>();
        }

        private void Update()
        {
            if (destroyWhenOutOfScreen && IsOutOfScreen())
            {
                gameObject.ReturnToPoolOrDestroy();
            }
        }

        public void SetDirection(Vector2 direction)
        {
            mover.SetDestination(direction * 1000);
        }

        private bool IsOutOfScreen()
        {
            Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
            return
                screenPosition.x < -outScreenOffSet.x || screenPosition.x > 1f + outScreenOffSet.x ||
                screenPosition.y < -outScreenOffSet.y || screenPosition.y > 1f + outScreenOffSet.y;
        }
    }
}