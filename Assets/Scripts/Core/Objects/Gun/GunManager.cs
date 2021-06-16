using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class GunManager : Singleton<GunManager>
    {
        [SerializeField] Gun[] gunPrefabs;
    }
}