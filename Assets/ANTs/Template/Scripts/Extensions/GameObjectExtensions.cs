using UnityEngine;
using ANTs.Template;
using System.Collections.Generic;
using System;

static public class GameObjectExtensions
{
    static public Dictionary<GameObject, ANTsPool> ObjectPools = new Dictionary<GameObject, ANTsPool>();
    static public Dictionary<GameObject, Action<object> > OnWakeUpEvents;
    static public Dictionary<GameObject, Action> OnSleepEvents;

    static public ANTsPool GetPool(this GameObject go)
    {
        return ObjectPools[go];
    }

    static public void SetPool(this GameObject go, ANTsPool objectPool)
    {
        ObjectPools[go] = objectPool;
    }

    static public void WakeUp(this GameObject go, object args)
    {
        OnWakeUpEvents[go]?.Invoke(args);
        go.SetActive(true);
    }

    static public void Sleep(this GameObject go)
    {
        OnSleepEvents[go]?.Invoke();
        go.SetActive(false);
    }

    static public void  SetWakeUpDelegate(this GameObject go, Action<object> callback)
    {
        OnWakeUpEvents[go] += callback;
    }

    static public void SetSleepDelegate(this GameObject go, Action callback)
    {
        OnSleepEvents[go] += callback;
    }

    static public void ReturnToPoolOrDestroy(this GameObject go)
    {
        if(ObjectPools.TryGetValue(go, out ANTsPool objectPool))
        {
            objectPool.ReturnToPool(go);            
        }
        else UnityEngine.Object.Destroy(go);
    }
}
