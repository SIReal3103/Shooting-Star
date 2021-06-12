using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Combat
{
    public class Damager : MonoBehaviour
    {
        private float criticalChance;
        private float critEfficent;
        private float lifeSteal; // Depends on damage dealt

        public float CriticalChance { get => criticalChance; set => criticalChance = value; }
        public float CritEfficent { get => critEfficent; set => critEfficent = value; }
        public float LifeSteal { get => lifeSteal; set => lifeSteal = value; }
    }
}