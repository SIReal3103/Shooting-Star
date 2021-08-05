using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Game
{
    [CreateAssetMenu(fileName = "New Health Upgrade", menuName = "ANTs/Health Upgrade", order = 0)]
    public class HealthUpgrade : UpgradeBase
    {
        [SerializeField] int healthBonus = 10;

        public override IEnumerable<float> GetBonus(StatType statType)
        {
            if (statType == StatType.Health)
            {
                yield return healthBonus;
            }
        }
    }
}