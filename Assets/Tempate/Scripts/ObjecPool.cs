using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Template
{
    [Serializable]
    public abstract class Pool<TPool, TObject, TInfo> : MonoBehaviour
        where TPool : Pool<TPool, TObject, TInfo>
        where TObject : PoolObject<TPool, TObject, TInfo>, new()
    {
        [SerializeField] GameObject prefab;

        List<TObject> pool = new List<TObject>();
        [SerializeField] int poolSize = 10;

        private void Start()
        {
            for (int i = 0; i < poolSize; i++)
            {
                TObject poolObject = CreateNewPoolObject();
                pool.Add(poolObject);
                Push(poolObject);
            }
        }

        private TObject CreateNewPoolObject()
        {
            TObject objectPool = new TObject();
            objectPool.instance = Instantiate(prefab, transform);
            objectPool.pool = this as TPool;

            return objectPool;
        }

        public void Push(TObject poolObject)
        {
            poolObject.InUse = false;
        }

        public TObject Pop()
        {
            TObject poolObject = GetIdlePoolObject();
            poolObject.InUse = true;

            return poolObject;
        }

        private TObject GetIdlePoolObject()
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!pool[i].InUse)
                {
                    return pool[i];
                }
            }

            Debug.LogError("Insufficient pool size");
            return null;
        }
    }

    [Serializable]
    public abstract class PoolObject<TPool, TObject, TInfo>
        where TPool : Pool<TPool, TObject, TInfo>
        where TObject : PoolObject<TPool, TObject, TInfo>, new()
    {
        public TPool pool;
        public GameObject instance;

        private bool inUse;

        public PoolObject()
        {
            inUse = true;
        }

        internal bool InUse
        {
            get => inUse;
            set
            {
                if (value == inUse) return;

                if (value)
                {
                    WakeUp();
                }
                else
                {
                    Sleep();
                }

                inUse = value;
            }
        }

        public abstract void Init(TInfo initializeValue);

        protected abstract void WakeUp();
        protected abstract void Sleep();

        public void ReturnToPool()
        {
            pool.Push(this as TObject);
        }
    }
}