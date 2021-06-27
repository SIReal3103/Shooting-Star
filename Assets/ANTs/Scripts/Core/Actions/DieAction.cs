using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class DieAction : ActionBase, IANTsPoolable<EnemyPool, DieAction>
    {
        [Header("Die Action")]
        [Space(10)]
        [Tooltip("How long before the return to pool")]
        [SerializeField] float durationReturnToPool = 1f;

        public EnemyPool CurrentPool { get; set; }

        protected override void ActionUpdate()
        {
            
        }

        public override void ActionStart()
        {
            base.ActionStart();
            Invoke(nameof(ReturnToPool), durationReturnToPool);
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