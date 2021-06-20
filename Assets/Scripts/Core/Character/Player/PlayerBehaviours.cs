﻿using UnityEngine;

namespace ANTs.Game
{
    public class PlayerBehaviours : MonoBehaviour
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
            mover.StartMovingTo(Position);
        }

        public void ChangeStrongerGunBehaviour()
        {
            gunner.ChangeToStrongerGun();
        }

        public void ChangeStrongerBulletBehaviour()
        {
            gunner.ChangeToStrongerBullet();
        }
    }
}