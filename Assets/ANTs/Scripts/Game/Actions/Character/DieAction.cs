using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class DieAction : ActionBase, IPoolable
    {
        [Header("Die Action")]
        [Space(10)]
        [Tooltip("How long before the return to pool")]
        [SerializeField] float timeBeforeReturnPool = 5f;
        [SerializeField] bool isDisableColliderWhenDie = true;

        public override void ActionStart()
        {
            base.ActionStart();
            if (isDisableColliderWhenDie)
            {
                GetComponent<Collider2D>().enabled = false;
            }

            Invoke(nameof(ReturnToPool), timeBeforeReturnPool);
        }

        public void ReturnToPool()
        {
            gameObject.ReturnToPoolOrDestroy();
        }

        public void WakeUp(object param)
        {
            EnemyData data = param as EnemyData;
            gameObject.SetActive(true);
            transform.position = data.spawnPosition;
        }

        public void Sleep() { }
    }

    public class EnemyData
    {
        public Vector3 spawnPosition;

        public EnemyData(Vector3 position)
        {
            this.spawnPosition = position;
        }
    }
}