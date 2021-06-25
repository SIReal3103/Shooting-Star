using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MoverAction))]
    public abstract class Projectile : MonoBehaviour
    {
        public GameObject source;

        private MoverAction mover;
        protected virtual void Awake()
        {
            mover = GetComponent<MoverAction>();
        }

        public void SetDirection(Vector2 direction)
        {
            mover.SetDestination(direction * 1000);
        }
    }
}