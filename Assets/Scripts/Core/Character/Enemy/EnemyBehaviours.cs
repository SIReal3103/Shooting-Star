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
    public class EnemyBehaviours : MonoBehaviour, IANTsPoolObject<EnemyPool, EnemyBehaviours>
    {
        [SerializeField] ANTsPolygon prepareZone;
        Vector2 preparePosition = Vector2.zero;

        Mover mover;

        public EnemyPool CurrentPool { get; set; }

        private void Start()
        {
            mover = GetComponent<Mover>();
            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Linearity);

            preparePosition = prepareZone.GetRandomPointOnSurface();
        }

        public void MoveToPrepareZone()
        {
            mover.StartMovingTo(preparePosition);
        }

        public void StopMoving()
        {
            mover.StopMoving();
        }

        public void Dead()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            CurrentPool.ReturnToPool(this);
        }

        // IANTsPoolObject implementation

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

