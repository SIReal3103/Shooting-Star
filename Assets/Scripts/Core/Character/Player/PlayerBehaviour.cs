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

        private void Start()
        {
            mover = GetComponent<Mover>();

            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Lerp);
        }

        public void MoveBehaviour(Vector2 Position)
        {
            mover.SetDestination(Position);
        }
    }
}