﻿using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public class ANTsPoolDecorator
    {
        public ANTsPool pool;
        public ProgressIdentifier currentLevelId;
        public ProgressIdentifier nextLevelId;

        public ANTsPoolDecorator(ANTsPool pool, ProgressIdentifier currentLevel, ProgressIdentifier nextLevel)
        {
            this.pool = pool;
            this.currentLevelId = currentLevel;
            this.nextLevelId = nextLevel;
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