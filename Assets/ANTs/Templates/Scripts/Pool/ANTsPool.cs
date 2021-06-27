using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public abstract class ANTsPool<TPool, TObject> : MonoBehaviour
        where TObject : MonoBehaviour, IANTsPoolable<TPool, TObject>
        where TPool : ANTsPool<TPool, TObject>
    {
        [SerializeField] private TObject prefab;
        [SerializeField] private int initialPoolSize = 10;

        private Queue<TObject> objects;

        public TObject Prefab { get => prefab; }

        public void ReloadPrefab(TObject prefab)
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
            objects = new Queue<TObject>();
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewPoolObject();
            }
        }

        public TObject Pop(System.Object args)
        {
            if (objects.Count == 0)
            {
                CreateNewPoolObject();
            }

            TObject obj = objects.Dequeue();
            obj.WakeUp(args);

            return obj;
        }

        public void ReturnToPool(TObject @object)
        {
            @object.transform.SetParent(transform);
            Push(@object);
        }

        private TObject CreateNewPoolObject()
        {
            TObject obj = Instantiate(Prefab, transform);
            obj.CurrentPool = this as TPool;
            Push(obj);
            return obj;
        }

        private void Push(TObject obj)
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

    public interface IANTsPoolable<TPool, TObject>
        where TObject : MonoBehaviour, IANTsPoolable<TPool, TObject>
        where TPool : ANTsPool<TPool, TObject>
    {
        TPool CurrentPool { get; set; }
        void ReturnToPool();
        void WakeUp(System.Object args);
        void Sleep();
    }
}



