using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public interface IAttackable
    {
        void OnAttack(GameObject attacker, Attack attack);
    }
}