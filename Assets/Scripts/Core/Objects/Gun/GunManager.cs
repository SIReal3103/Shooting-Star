using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Core
{
    public class GunManager : Singleton<GunManager>
    {
        ExpandedDictionary<Gun> gunDictionary;

        private void Start()
        {
            gunDictionary = new ExpandedDictionary<Gun>(gameObject);
        }

        public Gun GetStrongerGunFrom(Gun currentGun)
        {
            return gunDictionary.GetNextItem(currentGun);
        }
    }
}