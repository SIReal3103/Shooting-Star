using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{
    public class ExpandedDictionary<TObject>
        where TObject : MonoBehaviour
    {
        private Dictionary<string, TObject> dictionary;
        GameObject container;

        public TObject GetNextItem(TObject currentItem)
        {
            string currentKey = currentItem.name;
            return dictionary[GetNextKey(currentKey)];
        }

        private string GetNextKey(string currentKey)
        {
            List<string> keys = new List<string>(dictionary.Keys);
            int indexOfCurrentKey = keys.IndexOf(currentKey);
            return keys[GetNextIndexIfThereIs(indexOfCurrentKey)];
        }

        private int GetNextIndexIfThereIs(int index)
        {
            if (index + 1 == dictionary.Count)
            {
                Debug.Log("Can't get next index of the last item");
                return index;
            }

            return index + 1;
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
            if (!dictionary.TryGetValue(key, out value))
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
}