using System.Collections.Generic;
using UnityEngine;


public class ANTsPool<TPool, TObject> : MonoBehaviour
    where TObject : MonoBehaviour, IANTsPoolObject<TPool, TObject>
    where TPool : ANTsPool<TPool, TObject>
{
    [SerializeField] TObject prefab;
    [SerializeField] int initalPoolSize = 10;

    Queue<TObject> objects;

    private void Start()
    {
        objects = new Queue<TObject>();

        for(int i = 0; i < initalPoolSize; i++)
        {
            CreateNewPoolObject();
        }
    }

    public TObject Pop(System.Object args)
    {
        if(objects.Count == 0)
        {
            CreateNewPoolObject();
        }

        TObject obj = objects.Dequeue();
        obj.WakeUp(args);

        return obj;
    }

    public void ReturnToPool(TObject obj)
    {
        Push(obj);
    }

    private TObject CreateNewPoolObject()
    {
        TObject obj = Instantiate(prefab, transform);
        obj.CurrentPool = this as TPool;
        Push(obj);
        return obj;
    }

    private void Push(TObject obj)
    {
        objects.Enqueue(obj);
        obj.Sleep();
    }
}

public interface IANTsPoolObject<TPool, TObject>
    where TObject : MonoBehaviour, IANTsPoolObject<TPool, TObject>
    where TPool : ANTsPool<TPool, TObject>
{
    TPool CurrentPool { get; set; }
    void WakeUp(System.Object args);
    void Sleep();
}
