﻿using System.Collections.Generic;
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

        private void Awake()
        {
            foreach(CharacterClassQuery character in characterClassQueries)
            {
                statDict[character.characterClass] = new Dictionary<StatType, float[]>();
                foreach(StatTypeQuery stat in character.stats)
                {
                    statDict[character.characterClass].Add(stat.statType, stat.levels);
                }
            }
        }

        public float GetStat(CharacterClass characterClass, StatType statType, int level)
        {
            return statDict[characterClass][statType][level];
        }
    }
}