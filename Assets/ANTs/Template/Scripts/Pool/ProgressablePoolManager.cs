using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public class ANTsPoolDecorator
    {
        public ANTsPool pool;
        public string name;
        public ProgressIdentifier currentLevelId;
        public ProgressIdentifier nextLevelId;

        public ANTsPoolDecorator(ANTsPool pool, ProgressIdentifier currentLevel, ProgressIdentifier nextLevel, string name)
        {
            this.pool = pool;
            this.currentLevelId = currentLevel;
            this.nextLevelId = nextLevel;
            this.name = name;
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

        private Dictionary<string, ProgressIdentifier> name2Decorator =
            new Dictionary<string, ProgressIdentifier>();

        protected ProgressablePoolManager() { }

        protected override void Awake()
        {
            base.Awake();
            foreach (TObject prefab in prefabs)
            {
                ANTsPool pool = prefab.gameObject.GetOrCreatePool(transform);
                ANTsPoolDecorator decorator = new ANTsPoolDecorator(pool, prefab.CurrentLevel, prefab.NextLevel, prefab.name);
                pool2Decorator.Add(pool, decorator);
                id2Decorator.Add(prefab.CurrentLevel, decorator);
            }
        }

        public bool ProgressNextPool(ref ANTsPool pool)
        {
            if (pool2Decorator.TryGetValue(pool, out ANTsPoolDecorator decorator))
            {
                ANTsPoolDecorator nextDecorator = id2Decorator[decorator.nextLevelId];
                pool = nextDecorator.pool;
                return true;
            }
            else
            {
                Debug.LogWarning(pool + " is not found in " + this);
                return false;
            }
        }

        public ANTsPool GetPool(ProgressIdentifier id)
        {
            if (id2Decorator.TryGetValue(id, out ANTsPoolDecorator decorator))
            {
                return decorator.pool;
            }
            throw new UnityException("Invalid Id");
        }

        public ANTsPool GetDefaultPool()
        {
            return GetPool(defaultId);
        }
    }
}