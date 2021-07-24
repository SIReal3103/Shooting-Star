using System;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Game
{
    public class Upgrade : MonoBehaviour
    {
        public event Action OnUpgradeUpdateEvent;

        [SerializeField] List<UpgradeBase> upgrades;

        public List<UpgradeBase> GetUpgrades() { return upgrades; }

        private void Start()
        {
            OnUpgradeUpdateEvent?.Invoke();
        }

        public float GetBonus(StatType statType)
        {
            float result = 0;
            foreach (UpgradeBase upgrade in upgrades)
            {
                foreach (float bonus in upgrade.GetBonus(statType))
                {
                    result += bonus;
                }
            }
            return result;
        }
    }
}