using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class DieAction : ActionBase
    {
        [Header("Die Action")]
        [Space(10)]
        [Tooltip("How long before the return to pool")]
        [SerializeField] float timeBeforeReturnPool = 5f;
        [SerializeField] bool isDisableColliderWhenDie = true;

        protected override void Awake()
        {
            base.Awake();
            gameObject.SetWakeUpDelegate(args =>
            {
                EnemyData data = args as EnemyData;
                gameObject.SetActive(true);
                transform.position = data.spawnPosition;
            });
        }

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