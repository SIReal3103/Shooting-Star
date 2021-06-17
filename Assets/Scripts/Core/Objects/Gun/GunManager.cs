using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Core
{
    public class GunManager : Singleton<GunManager>
    {
        ExpandedDictionary<Gun> gunDictionary;
        int numberOfGuns;

        private void Start()
        {
            gunDictionary = new ExpandedDictionary<Gun>(gameObject);
            numberOfGuns = gunDictionary.Size();
        }

        public Gun GetStrongerGunFrom(Gun weakerGun)
        {
            return gunDictionary.GetValueFrom(GetNextGunKey(weakerGun.name));
        }

        private string GetNextGunKey(string key)
        {
            List<string> keys = new List<string>(gunDictionary.GetKeys());
            return keys[GetNextGunIndex(keys.IndexOf(key))];
        }

        private int GetNextGunIndex(int index)
        {
            if(index + 1 == numberOfGuns)
            {
                Debug.Log("Gun level max");
                return index;
            }

            return index + 1;
        }
    }
}