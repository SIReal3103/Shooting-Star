using ANTs.Template;
using System.Collections.Generic;
using UnityEngine;

static public class GameObjectPoolExtensions
{
    static public Dictionary<GameObject, ANTsPool> poolDict = new Dictionary<GameObject, ANTsPool>();

    /// <summary>
    /// Get pool the prefab belong to, automatically create one if don't have
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"> The parent for the new pool, null if don't specify </param>
    /// <returns>The pool this gameObject belong to</returns>
    static public ANTsPool CreatePool(this GameObject prefab, Transform parent = null)
    {                   
        ANTsPool result = new GameObject(prefab.name + "_pool").AddComponent<ANTsPool>();
        result.LoadNewPrefab(prefab);
        if (parent != null) result.transform.SetParentPreserve(parent);
        return result;
    }

    static public bool TryGetPool(this GameObject poolObject, out ANTsPool pool)
    {
        try
        {
            pool = GetPool(poolObject);
            return true;
        }
        catch(UnityException)
        {
            pool = null;
            return false;
        }
    }

    static public ANTsPool GetPool(this GameObject objectPool)
    {
        if (poolDict.TryGetValue(objectPool, out ANTsPool result))
        {
            return result;
        }
        throw new UnityException("No pool avaiable for " + objectPool);
    }

    static public void ReturnToPoolOrDestroy(this GameObject poolObject)
    {
        if (poolDict.TryGetValue(poolObject, out ANTsPool pool))
        {
            pool.ReturnToPool(poolObject);
        }
        else UnityEngine.Object.Destroy(poolObject);
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
}
