using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Game
{
    public class Upgrade : MonoBehaviour
    {
        [SerializeField] List<UpgradeBase> upgrades;

        public float GetBonus(StatType statType)
        {
            float result = 0;
            foreach(UpgradeBase upgrade in upgrades)
            {
                foreach(float bonus in upgrade.GetBonus(statType))
                {
                    result += bonus;
                }
            }
            return result;
        }
    }
}