using System;
using UnityEngine;

using ANTs.Template;

namespace ANTs.Core
{
    //TODO: Enemy not die

    [RequireComponent(typeof(Mover))]
    public class EnemyFacade : MonoBehaviour, IANTsPoolable<EnemyPool, EnemyFacade>
    {
        public EnemyPool CurrentPool { get; set; }

        #region =======================================BEHAVIOURS
        public void Dead()
        {
            if (CurrentPool == null) Destroy(this); // if gameObject is created directly on scene
            ReturnToPool();
        }
        #endregion

        #region =======================================IANTsPoolObject IMPLEMENTATION
        public void ReturnToPool()
        {
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

