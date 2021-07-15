using ANTs.Template;
using System.Collections.Generic;
using UnityEngine;

static public class GameObjectExtensions
{
    static public Dictionary<GameObject, ANTsPool> poolDict = new Dictionary<GameObject, ANTsPool>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="parent"> The parent for the new pool, null if don't specify </param>
    /// <returns>The pool this gameObject belong to</returns>
    static public ANTsPool GetOrCreatePool(this GameObject go, Transform parent = null)
    {
        if (!poolDict.TryGetValue(go, out ANTsPool result))
        {
            Debug.Log(go + " don't belong to any pool, one is automatically created on scene");
            GameObject newGo = new GameObject(go.name + "_pool");
            result = newGo.AddComponent<ANTsPool>();
            result.LoadNewPrefab(go);
        }

        if (parent != null) result.transform.SetParentPreserve(parent);
        return result;
    }

    static public ANTsPool GetPool(this GameObject go)
    {
        if (poolDict.TryGetValue(go, out ANTsPool result))
        {
            return result;
        }
        throw new UnityException("No pool avaiable for " + go);
    }

    static public void AddToDictionary(this GameObject go, ANTsPool pool)
    {
        if (poolDict.ContainsKey(go))
        {
            Debug.LogWarning(go + " already has a pool");
            poolDict[go] = pool;
        }
        else
        {
            poolDict.Add(go, pool);
        }
    }

    static public void ReturnToPoolOrDestroy(this GameObject go)
    {
        if (poolDict.TryGetValue(go, out ANTsPool objectPool))
        {
            objectPool.ReturnToPool(go);
        }
        else UnityEngine.Object.Destroy(go);
    }
}
