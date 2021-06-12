using UnityEngine;
using System.Collections.Generic;
namespace Game.Combat
{
    public class Attack
    {
        private readonly int _damage;
        private readonly bool _critical;
        public GameObject _effect;
        public string _soundName;

        public int _healthDrain;

        public Attack(int damage, bool critical, GameObject effect, string soundName, int healthDrain)
        {
            _damage = damage;
            _critical = critical;
            _effect = effect;
            _soundName = soundName;
            _healthDrain = healthDrain;
        }

        public int Damage
        {
            get { return _damage; }
        }

        public bool IsCritical
        {
            get { return _critical; }
        }

        public int HealthDrain
        {
            get { return _healthDrain; }
        }
    }
}