using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] bool tryUseBaseStat = true;
        [SerializeField] DamageData initialDamageData;

        [ReadOnly]
        [SerializeField] DamageData currentDamageData;

        public DamageData GetDamageData() { return currentDamageData; }

        private void Awake()
        {
            currentDamageData = GetInitialDamageData();
        }

        public void CombineDamageData(DamageData otherData, Damager damageCauser)
        {
            currentDamageData = GetInitialDamageData().Combine(otherData, damageCauser);
        }

        public Damager GetDamageCauser()
        {
            return currentDamageData.damageCauser;
        }

        private DamageData GetInitialDamageData()
        {
            if (tryUseBaseStat && TryGetComponent(out BaseStat baseStat))
            {
                return new DamageData(
                    baseStat.GetStat(StatType.DamageBonus),
                    baseStat.GetStat(StatType.DamageModifier),
                    this
                );
            }
            else
            {
                DamageData data = initialDamageData;
                data.damageCauser = this;
                return data;
            }
        }
    }

    [System.Serializable]
    public struct DamageData
    {
        public float damageBonus;
        public float damageModifier;
        [HideInInspector]
        public Damager damageCauser;

        public DamageData(float damageBonus, float damageModifier, Damager damageCauser)
        {
            this.damageBonus = damageBonus;
            this.damageModifier = damageModifier;
            this.damageCauser = damageCauser;
        }

        public DamageData Combine(DamageData damageData, Damager damageCauser)
        {
            return new DamageData(
                damageData.damageBonus + damageBonus,
                damageData.damageModifier + damageModifier,
                damageCauser);
        }
    }
}