using ANTs.Template;
using System;
using System.Collections.Generic;
using UnityEngine;

static public class GameObjectExtensions
{
    static public Dictionary<GameObject, ANTsPool> objectPools = new Dictionary<GameObject, ANTsPool>();
    static public Dictionary<GameObject, Action<object>> OnWakeUpEvents = new Dictionary<GameObject, Action<object>>();
    static public Dictionary<GameObject, Action> OnSleepEvents = new Dictionary<GameObject, Action>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="parent"> The parent for the new pool, null if don't specify </param>
    /// <returns>The pool this gameObject belong to</returns>
    static public ANTsPool GetOrCreatePool(this GameObject go, Transform parent = null)
    {
        if (objectPools.TryGetValue(go, out ANTsPool result))
        {

        }
        else
        {
            Debug.LogWarning(go + " don't belong to any pool, one is automatically created on scene");

            GameObject newGo = new GameObject(go.name + "_pool");
            if (parent != null) newGo.transform.SetParentPreserve(parent);
            result = newGo.AddComponent<ANTsPool>();
            result.LoadNewPrefab(go);
        }

        if (parent != null) result.transform.SetParentPreserve(parent);
        return result;
    }

    static public void SetPool(this GameObject go, ANTsPool pool)
    {
        if (objectPools.ContainsKey(go))
        {
            Debug.LogWarning(go + " already has a pool");
            objectPools[go] = pool;
        }
        else
        {
            objectPools.Add(go, pool);
        }
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
        if (objectPools.TryGetValue(go, out ANTsPool objectPool))
        {
            objectPool.ReturnToPool(go);
        }
        else UnityEngine.Object.Destroy(go);
    }
}
