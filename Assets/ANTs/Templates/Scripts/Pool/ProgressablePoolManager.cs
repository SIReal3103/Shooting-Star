using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public abstract class ProgressablePoolManager<TManager, TPool, TObject> : Singleton<TManager>
        where TManager : Component
        where TPool : ANTsPool<TPool, TObject>
        where TObject : MonoBehaviour, IANTsPoolable<TPool, TObject>, IProgressable
    {
        [SerializeField] ProgressIdentifier defaultId;
        [SerializeField] List<TObject> prefabs;

        private Dictionary<ProgressIdentifier, TPool> pools =
            new Dictionary<ProgressIdentifier, TPool>();

        protected ProgressablePoolManager() { }

        protected override void Awake()
        {
            foreach (TObject prefab in prefabs)
            {
                TPool pool = CreatePool(prefab);
                pool.ReloadPrefab(prefab);
                pools.Add(pool.Prefab.CurrentLevel, pool);
            }
        }

        private TPool CreatePool(TObject poolObject)
        {
            GameObject go = new GameObject(poolObject.name + "_pool");
            go.transform.SetParent(transform);
            return go.AddComponent<TPool>();
        }

        public bool ProgressNextPool(ref TPool pool)
        {
            if (pools.TryGetValue(pool.Prefab.NextLevel, out TPool result))
            {
                pool = result;
                return true;
            }
            Debug.Log("Can't get next of item level max.");
            return false;
        }

        public TPool GetDefaultPool()
        {
            TPool result;
            if (pools.TryGetValue(defaultId, out result))
            {
                return result;
            }
            throw new UnityException("Invalid default Id");
        }
    }
}