using System;
using UnityEngine;

using ANTs.Template;

namespace ANTs.Core
{
    //TODO: Enemy not die
    [RequireComponent(typeof(Damageable))]
    public class EnemyFacade : MonoBehaviour, IANTsPoolable<EnemyPool, EnemyFacade>
    {
        [Tooltip("How long the enemy stay dead before return to pool")]
        [SerializeField] float durationReturnToPool;

        public EnemyPool CurrentPool { get; set; }

        private Damageable damageable;

        #region ========================================Unity Events
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        private void Update()
        {
            if(damageable.IsDead())
            {
                Dead();
            }
        }
        #endregion

        #region =======================================BEHAVIOURS
        public void Dead()
        {
            Invoke(nameof(ReturnToPool), durationReturnToPool);
        }
        #endregion

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

