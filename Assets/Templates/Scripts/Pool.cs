using System;
using System.Collections.Generic;

using UnityEngine;


public abstract class Pool<TPool, TObject, TData> : MonoBehaviour
    where TPool : Pool<TPool, TObject, TData>
    where TObject : PoolObject<TPool, TObject, TData>, new()
{
    [SerializeField] GameObject prefab;

    List<TObject> pool = new List<TObject>();
    [SerializeField] int initialPoolSize = 10;

    private void Start()
    {
        for(int i = 0; i < initialPoolSize; i++)
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
        poolObject.InitPoolObject(this as TPool, Instantiate(prefab, transform));

        return poolObject;
    }

    public TObject Pop()
    {
        foreach(TObject poolObject in pool)
        {
            if(poolObject.IsInPool())
            {
                poolObject.SetInPool(false);
                return poolObject;
            }
        }

        return CreateNewPoolObject();
    }

    public void Push(TObject poolObject)
    {
        poolObject.SetInPool(true);
    }
}

public abstract class PoolObject<TPool, TObject, TData>
    where TPool : Pool<TPool, TObject, TData>
    where TObject : PoolObject<TPool, TObject, TData>, new()
{
    private TPool pool;
    private bool inPool;

    private GameObject instance;

    public GameObject GetInstance()
    {
        return instance;
    }

    public void InitPoolObject(TPool pool, GameObject instance)
    {
        this.pool = pool;
        this.instance = instance;
        SetInPool(false);
    }

    public bool IsInPool()
    {
        return inPool;
    }

    public void SetInPool(bool inPool)
    {
        if(inPool)
        {
            Sleep();
        }
        else
        {
            WakeUp();
        }

        this.inPool = inPool;
    }

    public abstract void InitData(TData data);

    public void ReturnToPool()
    {
        pool.Push(this as TObject);
    }

    protected virtual void WakeUp()
    {
        instance.SetActive(true);
    }

    protected virtual void Sleep()
    {
        instance.SetActive(false);
    }
}