using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Movement;

namespace Game.Core
{
    [RequireComponent(typeof(Mover))]
    public abstract class Projectile : MonoBehaviour
    {
        [HideInInspector]
        public GameObject source;

        Mover mover;

        private void OnEnable()
        {
            Debug.Log("Set " + name);
            mover = GetComponent<Mover>();
            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Linearity);
        }

        public void SetDirection(Vector2 direction)
        {
            mover.MoveStrategy.data.destination = direction * Mathf.Infinity;
        }
    }
}