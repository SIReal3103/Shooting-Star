using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public abstract class ANTsPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialPoolSize = 10;

        private Queue<GameObject> objects;

        public GameObject Prefab { get => prefab; }

        public void ReloadPrefab(GameObject prefab)
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
            objects = new Queue<GameObject>();
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewPoolObject();
            }
        }

        public GameObject Pop(object args)
        {
            if (objects.Count == 0)
            {
                CreateNewPoolObject();
            }

            GameObject obj = objects.Dequeue();
            obj.WakeUp(args);

            return obj;
        }

        public void ReturnToPool(GameObject poolObject)
        {
            poolObject.transform.SetParent(transform);
            Push(poolObject);
        }

        private GameObject CreateNewPoolObject()
        {
            GameObject obj = Instantiate(Prefab, transform);
            obj.SetPool(this);
            Push(obj);
            return obj;
        }

        private void Push(GameObject obj)
        {
            objects.Enqueue(obj);
            obj.Sleep();
        }

        private void ResetPool()
        {
            if (objects == null) return;
            while (objects.Count > 0)
            {
                Destroy(objects.Dequeue());
            }
            objects = null;
        }
    }
}



