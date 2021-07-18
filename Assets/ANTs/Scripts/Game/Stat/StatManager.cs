using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Game
{
    [System.Serializable]
    public enum StatType
    {
        Health,
        ExperienceToLevelUp,
        ExperienceToAward,
        DamageModifier,
        DamageBonus
    }

    [System.Serializable]
    public enum CharacterClass
    {
        Player,
        Shooter,
        Ninja
    }

    [CreateAssetMenu(fileName = "Stat", menuName = "ANTs/New Stat", order = 0)]
    public class StatManager : ScriptableObject
    {
        [System.Serializable]
        class CharacterClassQuery
        {
            public CharacterClass characterClass;
            public StatTypeQuery[] stats;
        }

        [System.Serializable]
        class StatTypeQuery
        {
            public StatType statType;
            public float[] levels;
        }

        [SerializeField]
        CharacterClassQuery[] characterClassQueries;

        private Dictionary<CharacterClass, Dictionary<StatType, float[]>> statDict =
            new Dictionary<CharacterClass, Dictionary<StatType, float[]>>();

        static private bool statDictBuilded = false;

        private void BuildStatDict()
        {
            if (statDictBuilded) return;
            statDictBuilded = true;

            foreach (CharacterClassQuery character in characterClassQueries)
            {
                statDict[character.characterClass] = new Dictionary<StatType, float[]>();
                foreach (StatTypeQuery stat in character.stats)
                {
                    statDict[character.characterClass].Add(stat.statType, stat.levels);
                }
            }
        }

        public float GetStat(CharacterClass characterClass, StatType statType, int level)
        {
            BuildStatDict();

            return statDict[characterClass][statType][level - 1];
        }

        public float[] GetLevels(CharacterClass characterClass, StatType statType)
        {
            BuildStatDict();
            return statDict[characterClass][statType];
        }
    }
}