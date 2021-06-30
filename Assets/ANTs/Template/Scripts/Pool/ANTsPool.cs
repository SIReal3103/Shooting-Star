using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public abstract class ANTsPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialPoolSize = 10;

        private Queue<GameObject> objects;
        //public GameObject Prefab { get => prefab; }

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
                CreateNewPoolObject();
        }

        public GameObject Pop(object args)
        {
            if (objects.Count == 0)
                CreateNewPoolObject();

            GameObject obj = objects.Dequeue();
            obj.WakeUp(args);

            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.transform.SetParentPreserve(transform);
            objects.Enqueue(obj);
            obj.Sleep();
        }

        private GameObject CreateNewPoolObject()
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParentPreserve(transform);
            go.SetPool(this);
            objects.Enqueue(go);
            go.SetActive(false);
            return go;
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



