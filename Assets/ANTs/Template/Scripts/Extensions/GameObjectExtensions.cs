using UnityEngine;
using ANTs.Template;
using System.Collections.Generic;
using System;

static public class GameObjectExtensions
{
    static public Dictionary<GameObject, ANTsPool> objectPools = new Dictionary<GameObject, ANTsPool>();
    static public Dictionary<GameObject, Action<object>> OnWakeUpEvents = new Dictionary<GameObject, Action<object>>();
    static public Dictionary<GameObject, Action> OnSleepEvents = new Dictionary<GameObject, Action>();

    static public ANTsPool GetPool(this GameObject go)
    {
        return objectPools[go];
    }

    static public void SetPool(this GameObject go, ANTsPool objectPool)
    {
        objectPools.Add(go, objectPool);
    }

    static public void WakeUp(this GameObject go, object args)
    {
        if (OnWakeUpEvents.TryGetValue(go, out Action<object> @event))
            @event?.Invoke(args);
        go.SetActive(true);
    }

    static public void Sleep(this GameObject go)
    {
        if (OnSleepEvents.TryGetValue(go, out Action @event))
            @event?.Invoke();
        go.SetActive(false);
    }

    static public void SetWakeUpDelegate(this GameObject go, Action<object> callback)
    {
        if (OnWakeUpEvents.ContainsKey(go))
        {
            Debug.LogWarning("WakeUp is delegate multiple time in " + go);
            OnWakeUpEvents[go] += callback;
        }
        else
        {
            OnWakeUpEvents.Add(go, callback);
        }            
    }
    static public void SetSleepDelegate(this GameObject go, Action callback)
    {
        if (OnSleepEvents.ContainsKey(go))
        {
            Debug.LogWarning("Sleep is delegate multiple time in " + go);
            OnSleepEvents[go] += callback;
        }
        else
        {
            OnSleepEvents.Add(go, callback);
        }
    }

    static public void ReturnToPoolOrDestroy(this GameObject go)
    {
        if(objectPools.TryGetValue(go, out ANTsPool objectPool))
        {
            objectPool.ReturnToPool(go);            
        }
        else UnityEngine.Object.Destroy(go);
    }
}
