﻿using System.Collections;
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
            numberOfGuns = transform.childCount;
        }

        public Gun GetStrongerGunFrom(Gun weakerGun)
        {
            return gunDictionary.GetValueFrom(GetNextKey(weakerGun.name));
        }

        private string GetNextKey(string key)
        {
            List<string> keyList = gunDictionary.keys;
            return keyList[GetNextGunIndex(keyList.IndexOf(key))];
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