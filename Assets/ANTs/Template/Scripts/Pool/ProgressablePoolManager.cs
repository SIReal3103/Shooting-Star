using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    //public abstract class ProgressablePoolManager<TManager, TPool, TObject> : Singleton<TManager>
    //    where TManager : Component
    //    where TPool : ANTsPool<TPool, TObject>
    //    where TObject : MonoBehaviour, IANTsPoolable<TPool, TObject>, IProgressable
    //{

    public class ANTsPoolDecorator
    {
        public ANTsPool pool;
        public ProgressIdentifier currentLevel;
        public ProgressIdentifier nextLevel;

        public ANTsPoolDecorator(ANTsPool pool, ProgressIdentifier currentLevel, ProgressIdentifier nextLevel)
        {
            this.pool = pool;
            this.currentLevel = currentLevel;
            this.nextLevel = nextLevel;
        }
    }

    public abstract class ProgressablePoolManager<TManager, TObject> : Singleton<TManager>
        where TManager : Component
        where TObject : Component, IProgressable
    {
        [SerializeField] ProgressIdentifier defaultId;
        [SerializeField] List<TObject> prefabs;

        private Dictionary<ProgressIdentifier, ANTsPoolDecorator> id2Decorator =
            new Dictionary<ProgressIdentifier, ANTsPoolDecorator>();

        private Dictionary<ANTsPool, ANTsPoolDecorator> pool2Decorator =
            new Dictionary<ANTsPool, ANTsPoolDecorator>();

        protected ProgressablePoolManager() { }

        protected override void Awake()
        {
            base.Awake();
            foreach (TObject prefab in prefabs)
            {
                ANTsPool pool = prefab.gameObject.GetOrCreatePool(transform);
                ANTsPoolDecorator decorator = new ANTsPoolDecorator(pool, prefab.CurrentLevel, prefab.NextLevel);
                pool2Decorator.Add(pool, decorator);
                id2Decorator.Add(prefab.CurrentLevel, decorator);
            }
        }

        //private TPool CreatePool(TObject poolObject)
        //{
        //    GameObject go = new GameObject(poolObject.name + "_pool");
        //    go.transform.SetParentPreserve(transform);
        //    return go.AddComponent<TPool>();
        //}

        public bool ProgressNextPool(ref ANTsPool pool)
        {
            if (pool2Decorator.TryGetValue(pool, out ANTsPoolDecorator decorator))
            {
                ANTsPoolDecorator nextDecorator = id2Decorator[decorator.nextLevel];
                pool = nextDecorator.pool;
                return true;
            }
            else
            {
                Debug.LogWarning(pool + " is not found in " + this);
                return false;
            }

            //ProgressIdentifier id = (pool.GetPrefab() as IProgressable).CurrentLevel;
            //if (id2Decorators.TryGetValue(pool.Prefab.NextLevel, out ANTsPool result))
            //{
            //    pool = result;
            //    return true;
            //}
            //Debug.Log("Can't get next of item level max.");
            //return false;
        }

        public ANTsPool GetDefaultPool()
        {
            if (id2Decorator.TryGetValue(defaultId, out ANTsPoolDecorator decorator))
            {
                return decorator.pool;
            }
            throw new UnityException("Invalid default Id");
        }
    }
}