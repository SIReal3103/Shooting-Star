using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] DamageData initialDamageData = null;

        [ReadOnly]
        [SerializeField]
        DamageData currentDamageData;

        public DamageData GetDamageData() { return currentDamageData; }

        public void AddToDamageData(DamageData additiveData)
        {
            currentDamageData = GetInitialDamageData().Combine(additiveData);
        }

        private void Awake()
        {
            currentDamageData = GetInitialDamageData();
        }

        private DamageData GetInitialDamageData()
        {
            if (TryGetComponent(out BaseStat baseStat))
            {
                return new DamageData(
                    baseStat.GetStat(StatType.DamageBonus),
                    baseStat.GetStat(StatType.DamageModifier)
                );
            }
            else
            {
                return initialDamageData.Clone();
            }
        }

        public float GetFinalDamage()
        {
            return currentDamageData.damageBonus * (1f + currentDamageData.damageModifier);
        }

        public bool IsEnemy(Damageable damageable)
        {
            return damageable.tag != tag;
        }
    }

    [System.Serializable]
    public class DamageData
    {
        public float damageBonus = 10;
        public float damageModifier = 0;

        public DamageData(float damageBonus, float damageModifier)
        {
            this.damageBonus = damageBonus;
            this.damageModifier = damageModifier;
        }

        public DamageData Combine(DamageData damageData)
        {
            return new DamageData(damageData.damageBonus + damageBonus, damageData.damageModifier + damageModifier);
        }

        public DamageData Clone()
        {
            return new DamageData(damageBonus, damageModifier);
        }
    }
}