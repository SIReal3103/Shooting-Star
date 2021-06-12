using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(fileName = "Projectile.asset", menuName = "Attack/ProjectileAttack")]
    public class ProjectileFactory : AttackFactory
    {
        public Projectile ProjectToFire;

        public void Fire(GameObject attacker, Vector3 direction)
        {
            // Create and fire to target
            Projectile projectile = Instantiate(ProjectToFire, attacker.transform.position, Quaternion.identity);

            projectile.Fire(attacker, direction);
        }
    }
}