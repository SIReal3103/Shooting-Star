using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class AttackFactory : MonoBehaviour
    {
        public int minDamageBonus;
        public int maxDamageBonus;

        public GameObject Effect;
        public string SoundName;

        public Attack CreateAttack(GameObject Attacker, GameObject Defender)
        {
            CharacterStats AttackerStats = Attacker.GetComponent<CharacterStats>();
            CharacterStats DefenderStats = Defender.GetComponent<CharacterStats>();

            ///
            if (AttackerStats == null || DefenderStats == null)
            {
                Debug.Log("Attacker or Defender dont have Stats !!!");
                return null;
            }

            // DAMAGE
            int coreDamage = AttackerStats.GetDamage();
            coreDamage += Random.Range(minDamageBonus, maxDamageBonus + 1);

            bool isCritical = Random.value < (float)(AttackerStats.GetCritChance());
            if (isCritical)
                coreDamage = (int)(coreDamage * (int)(AttackerStats.GetCritEfficiency()));

            if (DefenderStats != null)
                coreDamage -= DefenderStats.GetDefent();

            int healthDrain = (int) (coreDamage * (AttackerStats.GetHealthDrain()));

            Attack attackTmp = new Attack(coreDamage, isCritical, Effect, SoundName, healthDrain);

            return attackTmp;
        }
    }
}