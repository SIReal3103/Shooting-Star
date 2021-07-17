using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(MoveAction))]
    public abstract class Projectile : MonoBehaviour
    {
        private MoveAction mover;
        protected virtual void Awake()
        {
            mover = GetComponent<MoveAction>();
        }

        public void SetDirection(Vector2 direction)
        {
            mover.SetDestination(direction * 1000);
        }
    }
}