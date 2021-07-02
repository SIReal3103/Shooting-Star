using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    //public abstract class ProgressablePoolManager<TManager, TPool, TObject> : Singleton<TManager>
    //    where TManager : Component
    //    where TPool : ANTsPool<TPool, TObject>
    //    where TObject : MonoBehaviour, IANTsPoolable<TPool, TObject>, IProgressable
    //{

    public abstract class ProgressablePoolManager<TManager, TObject> : Singleton<TManager>
        where TManager : Component
        where TObject : MonoBehaviour, IProgressable
    {
        [SerializeField] ProgressIdentifier defaultId;
        [SerializeField] List<TObject> prefabs;

        private Dictionary<ProgressIdentifier, ANTsPool> pools =
            new Dictionary<ProgressIdentifier, ANTsPool>();

        protected ProgressablePoolManager() { }

        protected override void Awake()
        {
            foreach (TObject prefab in prefabs)
            {
                ANTsPool pool = prefab.gameObject.GetOrCreatePool(transform);
                pools.Add(prefab.CurrentLevel, pool);
            }
        }

        //private TPool CreatePool(TObject poolObject)
        //{
        //    GameObject go = new GameObject(poolObject.name + "_pool");
        //    go.transform.SetParentPreserve(transform);
        //    return go.AddComponent<TPool>();
        //}

        //public bool ProgressNextPool(ref TPool pool)
        //{
        //    if (pools.TryGetValue(pool.Prefab.NextLevel, out TPool result))
        //    {
        //        pool = result;
        //        return true;
        //    }
        //    Debug.Log("Can't get next of item level max.");
        //    return false;
        //}

        //public TPool GetDefaultPool()
        //{
        //    TPool result;
        //    if (pools.TryGetValue(defaultId, out result))
        //    {
        //        return result;
        //    }
        //    throw new UnityException("Invalid default Id");
        //}
    }
}