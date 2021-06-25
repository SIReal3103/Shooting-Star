using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Mover))]
    public abstract class Projectile : MonoBehaviour
    {
        public GameObject source;

        private Mover mover;
        protected virtual void Awake()
        {
            mover = GetComponent<Mover>();
        }

        public void SetDirection(Vector2 direction)
        {
            mover.SetDestination(direction * 1000);
        }
    }
}