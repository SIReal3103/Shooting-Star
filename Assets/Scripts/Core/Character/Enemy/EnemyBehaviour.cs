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
    public class EnemyBehaviour : MonoBehaviour, IANTsPoolObject<EnemyPool, EnemyBehaviour>
    {
        Mover mover;

        public EnemyPool CurrentPool { get; set; }

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
            CurrentPool.ReturnToPool(this);
        }

        public void WakeUp(object args)
        {
            EnemyData data = args as EnemyData;
            gameObject.SetActive(true);
            transform.position = data.spawnPosition;
        }

        public void Sleep()
        {
            gameObject.SetActive(false);
        }
    }
}

