using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "asset", menuName = "Character/Stats")]
public class StatsDefinition : ScriptableObject
{
    public int Health;

    public int Damage;
    public int Defent;
    public int Dodge;   

    [Header("Percent %")]
    public int CriticalChance;
    public int CritEfficent;
    public float HealthDrain;

}
