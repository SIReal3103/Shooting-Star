using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Combat;
using Game.Movement;

namespace Game.Core
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Mover))]
    public class EnemyBehaviour : MonoBehaviour
    {
        // Pooling
        [HideInInspector]
        public EnemyObject enemyObject;

        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Linearity);
        }

        private void Update()
        {
            if(GetComponent<Damageable>().IsDead())
            {
                DeadBehaviour();
            }
        }

        public void MoveBehaviour(Vector2 position)
        {
            mover.SetDestination(position);
        }

        private void DeadBehaviour()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            enemyObject.ReturnToPool();
        }    
    }
}