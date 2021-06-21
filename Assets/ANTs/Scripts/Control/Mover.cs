﻿using System;
using UnityEngine;

namespace ANTs.Game
{
    public enum MovementType
    {
        Linearity,
        Lerp
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        public event Action onArrivedEvent;

        [SerializeField] MoveData initialData;

        private Rigidbody2D rb;
        private MoveStrategy moveStrategy;
        private bool isStop = true;


        public MoveStrategy MoveStrategy
        {
            get => moveStrategy;
            set { moveStrategy = value; LoadDataToStrategy(); }
        }

        private void Start()
        {
            this.rb = GetComponent<Rigidbody2D>();
            initialData.rb = this.rb;
        }

        private void Update()
        {
            if (isStop) return;

            MoveStrategy?.UpdatePath();
            if (Arrived())
            {
                onArrivedEvent?.Invoke();
                StopMoving();
            }
        }

        private bool Arrived()
        {
            return (moveStrategy.data.destination - rb.position).magnitude < moveStrategy.data.DestinationOffset;
        }

        private void LoadDataToStrategy()
        {
            moveStrategy.data = initialData;
        }

        public void StopMoving()
        {
            isStop = true;
        }

        public void StartMovingTo(Vector2 destination)
        {
            isStop = false;
            moveStrategy.data.destination = destination;
        }
    }

    public abstract class MoveFactory
    {
        private MoveFactory() { }

        public static MoveStrategy CreateMove(MovementType moveType)
        {
            switch (moveType)
            {
                case MovementType.Linearity:
                    return new MoveLinearity();
                case MovementType.Lerp:
                    return new LerpMovement();
                default:
                    throw new UnityException("Invalid move strategy");
            }
        }
    }

    public abstract class MoveStrategy
    {
        public MoveData data;
        public abstract void UpdatePath();
    }

    public class MoveLinearity : MoveStrategy
    {
        public override void UpdatePath()
        {
            Vector2 direction = (data.destination - data.rb.position).normalized;
            data.rb.MovePosition(data.rb.position + data.Speed * Time.deltaTime * direction);
        }
    }

    public class LerpMovement : MoveStrategy
    {
        public override void UpdatePath()
        {
            data.rb.MovePosition(Vector2.Lerp(data.rb.position, data.destination, data.TiltSpeed * Time.deltaTime));
        }
    }

    [System.Serializable]
    public class MoveData
    {
        [SerializeField] float speed = 10f;
        [SerializeField] float tiltSpeed = 0f;
        [SerializeField] float destinationOffset = 0.1f;

        [HideInInspector]
        public Vector2 destination;
        [HideInInspector]
        public Rigidbody2D rb;
        public float Speed { get => speed; }
        public float TiltSpeed { get => tiltSpeed; }
        public float DestinationOffset { get => destinationOffset; }
    }
}