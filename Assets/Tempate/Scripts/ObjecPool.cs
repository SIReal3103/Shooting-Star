using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Template
{
    public abstract class Pool<TPool, TObject, TData> : MonoBehaviour
        where TPool : Pool<TPool, TObject, TData>
        where TObject : PoolObject<TPool, TObject, TData>, new()
    {
        [SerializeField] GameObject prefab;

        List<TObject> pool = new List<TObject>();
        [SerializeField] int poolSize = 10;

        private void Start()
        {
            for(int i = 0; i < poolSize; i++)
            {
                AddToPool(CreateNewPoolObject());
            }
        }

        private void AddToPool(TObject poolObject)
        {
            pool.Add(poolObject);
            Push(poolObject);
        }

        private TObject CreateNewPoolObject()
        {
            TObject poolObject = new TObject();
            poolObject.instance = Instantiate(prefab, transform);
            poolObject.pool = this as TPool;

            return poolObject;
        }

        public TObject Pop()
        {
            foreach(TObject poolObject in pool)
            {
                if(poolObject.inPool)
                {
                    poolObject.inPool = false;
                    poolObject.WakeUp();
                    return poolObject;
                }
            }

            Debug.LogError("Pool empty");
            return null;
        }

        public void Push(TObject poolObject)
        {
            poolObject.inPool = true;
            poolObject.Sleep();
        }
    }

    public abstract class PoolObject<TPool, TObject, TData>
        where TPool : Pool<TPool, TObject, TData>
        where TObject : PoolObject<TPool, TObject, TData>, new()
    {
        public TPool pool;
        public GameObject instance;
        public bool inPool;

        public abstract void Init(TData data);


        public abstract void WakeUp();
        public abstract void Sleep();

        public void ReturnToPool()
        {
            pool.Push(this as TObject);
        }
    }
}