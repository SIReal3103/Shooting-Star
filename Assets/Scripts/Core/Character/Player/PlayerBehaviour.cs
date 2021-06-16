using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Movement;

namespace Game.Core
{
    public class PlayerBehaviour : MonoBehaviour
    {
        Mover mover;
        Gunner gunner;

        private void Start()
        {
            mover = GetComponent<Mover>();
            gunner = GetComponent<Gunner>();

            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Lerp);
        }

        public void MoveBehaviour(Vector2 Position)
        {
            mover.SetDestination(Position);
        }

        internal void ChangeGunBehaviour()
        {
            
        }
    }
}