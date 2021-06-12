using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    StatsDefinition stats;

    public int GetDamage()
    {
        return stats.Damage;
    }
    public int GetCritChance()
    {
        return (int)(stats.CriticalChance / 100f);
    }
    public int GetCritEfficiency()
    {
        return (int)(stats.CritEfficent / 100f);
    } 
    public int GetDefent()
    {
        return stats.Defent;
    }
    public float GetHealthDrain()
    {
        return (stats.HealthDrain / 100f); ;
    }
}
