using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class DieAction : ActionBase, IANTsPoolable<EnemyPool, DieAction>
    {
        [Header("Die Action")]
        [Space(10)]
        [Tooltip("How long before the return to pool")]
        [SerializeField] float timeBeforeReturnPool = 5f;
        [SerializeField] bool isDisableColliderWhenDie = true;

        public EnemyPool CurrentPool { get; set; }

        public override void ActionStart()
        {
            base.ActionStart();
            if (isDisableColliderWhenDie)
            {
                GetComponent<Collider2D>().enabled = false;
            }

            Invoke(nameof(ReturnToPool), timeBeforeReturnPool);
        }

        #region =======================================IANTsPoolObject IMPLEMENTATION
        public void ReturnToPool()
        {
            if (CurrentPool == null)
            {
                Destroy(gameObject);
                return;
            }
            CurrentPool.ReturnToPool(this);
        }

        public void WakeUp(object args)
        {
            EnemyData data = args as EnemyData;
            gameObject.SetActive(true);
            transform.position = data.spawnPosition;
        }

        public void Sleep()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}