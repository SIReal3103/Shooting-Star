using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Combat;

namespace Game.Core
{
    [RequireComponent(typeof(EnemyBehaviours))]
    [RequireComponent(typeof(Damageable))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] ANTsPolygon PlayGround;

        EnemyBehaviours enemy;

        private void Start()
        {
            enemy = GetComponent<EnemyBehaviours>();
        }

        private void Update()
        {
            if(GetComponent<Damageable>().IsDead())
            {
                enemy.Dead();
            }
        }
    }
}