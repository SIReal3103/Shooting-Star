using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Game
{
    public abstract class UpgradeBase : ScriptableObject
    {
        [SerializeField] public Sprite icon;

        public abstract IEnumerable<float> GetBonus(StatType statType);
    }
}
