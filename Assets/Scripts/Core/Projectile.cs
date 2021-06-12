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
        public Vector2 moveDirection;
        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            mover.MoveWith(moveDirection);
        }
    }
}