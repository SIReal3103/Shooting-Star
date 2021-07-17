using UnityEngine;

namespace ANTs.Game
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] float damageBonusOverride = 10;
        [SerializeField] float damageModifierOverride = 1;

        public bool IsEnemy(Damageable damageable)
        {
            return damageable.tag != tag;
        }

        public float CalculateFinalDamage()
        {
            float totalDamageBonus = 0;
            float totalDamageModifier = 0;

            for (Transform currentTransform = transform; currentTransform != null; currentTransform = currentTransform.parent)
            {
                if (!TryGetComponent(out Damager damager)) continue;
                totalDamageBonus += GetDamageStat(damager, StatType.DamageBonus);
                totalDamageModifier += GetDamageStat(damager, StatType.DamageModifier);                
            }

            return 0;
        }

        private float GetDamageStat(Damager damager, StatType damageStat)
        {
            if (!damager.TryGetComponent(out BaseStat baseStat)) return 0;
            return baseStat.GetStat(damageStat);
        }
    }
}