using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public class ANTsPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialPoolSize = 10;

        private Queue<GameObject> pool;
        public GameObject GetPrefab() { return prefab; }

        public void LoadNewPrefab(GameObject prefab)
        {
            this.prefab = prefab;
            ResetPool();
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

            GameObject obj = pool.Dequeue();
            WakeUpWrapper(obj, param);
            return obj;
        }

        public void ReturnToPool(GameObject objectPool)
        {
            objectPool.transform.SetParentPreserve(transform);
            pool.Enqueue(objectPool);
            SleepWrapper(objectPool);
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
            GameObject objectPool = Instantiate(prefab);
            objectPool.transform.SetParentPreserve(transform);
            objectPool.AddToDictionary(this);
            pool.Enqueue(objectPool);
            objectPool.SetActive(false);
            return objectPool;
        }

        private void ResetPool()
        {
            if (pool == null) return;
            while (pool.Count > 0)
            {
                Destroy(pool.Dequeue());
            }
            pool = null;
        }
    }
}



