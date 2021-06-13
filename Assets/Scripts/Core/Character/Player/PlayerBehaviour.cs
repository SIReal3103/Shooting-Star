using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Movement;

namespace Game.Core
{
    [RequireComponent(typeof(Character))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField]
        BulletPool currentPool;
        Character character;

        [SerializeField] float timeBetweenFire = 0.5f;
        float timeSinceLastFire = Mathf.Infinity;

        Mover mover;

        private void Start()
        {
            character = GetComponent<Character>();
            mover = GetComponent<Mover>();

            mover.MoveStrategy = MoveFactory.CreateMove(MovementType.Lerp);
        }

        private void Update()
        {
            MoveBehaviour(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            FireBehaviour();

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }

        public void MoveBehaviour(Vector2 Position)
        {
            mover.SetDestination(Position);
        }

        public void FireBehaviour()
        {
            if (timeSinceLastFire > timeBetweenFire)
            {
                BulletData bulletData = new BulletData(gameObject, character.GetBulletSpawnPosition(), Vector2.up);
                currentPool.Pop().InitData(bulletData);

                timeSinceLastFire = 0;
            }
        }
    }
}