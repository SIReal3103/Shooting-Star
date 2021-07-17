using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public class ANTsPool : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] int initialPoolSize = 10;

        private Queue<GameObject> pool;
        public GameObject GetPrefab() { return prefab; }

        public void LoadNewPrefab(GameObject prefab)
        {
            this.prefab = prefab;
            Init();
        }

        private void Awake()
        {
            if (prefab != null) Init();
        }

        private void Init()
        {
            pool = new Queue<GameObject>();
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewPoolObject();
            }
        }

        public GameObject Pop(object param)
        {
            if (pool.Count == 0) CreateNewPoolObject();
            GameObject poolObject = pool.Dequeue();
            WakeUpWrapper(poolObject, param);
            return poolObject;
        }

        private void Push(GameObject objectPool)
        {
            pool.Enqueue(objectPool);
            SleepWrapper(objectPool);
        }

        public void ReturnToPool(GameObject objectPool)
        {
            objectPool.transform.SetParentPreserve(transform);
            Push(objectPool);
        }

        private void WakeUpWrapper(GameObject objectPool, object param)
        {
            objectPool.SetActive(true);
            objectPool.GetComponent<IPoolable>().WakeUp(param);
        }

        private void SleepWrapper(GameObject objectPool)
        {
            objectPool.SetActive(false);
            objectPool.GetComponent<IPoolable>().Sleep();
        }

        private GameObject CreateNewPoolObject()
        {
            if (prefab.GetComponent<IPoolable>() == null)
            {
                throw new UnityException(prefab + " is not a Poolable object.");
            }

            GameObject objectPool = Instantiate(prefab);
            objectPool.transform.SetParentPreserve(transform);
            objectPool.AddToPoolDict(this);

            Push(objectPool);
            return objectPool;
        }
    }
}



