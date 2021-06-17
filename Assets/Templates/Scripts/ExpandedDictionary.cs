using System.Collections.Generic;
using UnityEngine;

public class ExpandedDictionary<TObject>
    where TObject : MonoBehaviour
{
    private Dictionary<string, TObject> dictionary;
    GameObject container;

    public ICollection<string> GetKeys()
    {
        return dictionary.Keys;
    }

    public ExpandedDictionary(GameObject container)
    {
        dictionary = new Dictionary<string, TObject>();

        this.container = container;
        LoadContainer();
    }

    void LoadContainer()
    {
        Transform transform = container.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            TObject child = transform.GetChild(i).GetComponent<TObject>();

            dictionary.Add(child.name, child);
        }
    }

    public TObject GetValueFrom(string key)
    {
        TObject value;
        if(!dictionary.TryGetValue(key, out value))
        {
            throw new UnityException("No value in container for key: " + key);
        }
        return value;
    }

    public int Size()
    {
        return dictionary.Count;
    }
}
