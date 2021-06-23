using UnityEngine;
using ANTs.Template;
using System.Collections.Generic;

namespace ANTs.Template
{
    public class ProgressablePoolManager<TManager, TPool, TObject> : Singleton<TManager>
        where TManager : Component
        where TPool : ANTsPool<TPool, TObject>
        where TObject : MonoBehaviour, IANTsPoolable<TPool, TObject>, IProgressable
    {
        [SerializeField] ProgressId defaultId;
        [SerializeField] List<TObject> prefabs;

        private Dictionary<ProgressId, TPool> pools =
            new Dictionary<ProgressId, TPool>();

        private TPool CreatePool(TObject poolObject)
        {
            GameObject go = new GameObject(poolObject.name + "_pool");
            go.transform.SetParent(transform);
            return go.AddComponent<TPool>();
        }

        protected ProgressablePoolManager() { }

        private void Start()
        {
            foreach (TObject prefab in prefabs)
            {
                TPool pool = CreatePool(prefab);
                pool.Prefab = prefab;
                pools.Add(pool.Prefab.CurrentLevel, pool);
            }
        }

        public TPool ProgressNextPool(TPool pool)
        {
            TPool result;
            if(pools.TryGetValue(pool.Prefab.NextLevel, out result))
            {
                return result;
            }
            Debug.Log("Item level max!");
            return pool;
        }

        public TPool GetDefaultPool()
        {
            return pools[defaultId];
        }
    }
}