using UnityEngine;

namespace ANTs.Game
{
    public class Damager : MonoBehaviour
    {
        const float NONE_VALUE = -1;

        [SerializeField]
        private DamageData damageData = null;

        public DamageData GetDamageData() { return damageData; }
        public void AddToDamageData(DamageData damageData) { this.damageData = this.damageData.Combine(damageData); }

        private void Awake()
        {
            if(TryGetComponent(out BaseStat baseStat))
            {
                damageData = new DamageData(
                    baseStat.GetStat(StatType.DamageBonus), 
                    baseStat.GetStat(StatType.DamageModifier)
                );
            }
        }

        public float GetFinalDamage()
        {
            return damageData.damageBonus * (1f + damageData.damageModifier);
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
    }
}