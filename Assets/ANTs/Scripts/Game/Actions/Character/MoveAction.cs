﻿using ANTs.Template;
using System;
using UnityEngine;
namespace ANTs.Game
{
    public enum MovementType
    {
        Linearity,
        Lerp
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveAction : ActionBase
    {
        [SerializeField] bool FacingWithDirection;
        [Conditional("FacingWithDirection", true)]
        [SerializeField] Transform model;
        [Space(10)]
        [SerializeField] MoveData initialMoveData;
        [Space(5)]
        [SerializeField] float destinationOffset = 0.5f;

        private Rigidbody2D rb;
        private MoveStrategy currentMove;

        private Action CurrentArrivedCallBack;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            SetMoveData(initialMoveData);
        }

        public void StartMovingTo(Vector2 destination, Action OnArrivedCallBack = null)
        {
            ActionStart();
            SetDestination(destination);

            CurrentArrivedCallBack?.Invoke();
            CurrentArrivedCallBack = OnArrivedCallBack;
        }

        public Vector2 GetMoveDirection()
        {
            return currentMove.data.GetMoveDirection();
        }

        public void SetMoveData(MoveData data)
        {
            data.rb = rb;
            currentMove = MoveFactory.CreateMove(data);
        }

        private void SetDestination(Vector2 destination)
        {
            currentMove.SetDestination(destination);
        }

        private bool IsMovingLeft()
        {
            return GetMoveDirection().x < 0f;
        }

        protected override void ActionFixedUpdate()
        {
            if (FacingWithDirection)
            {
                model.right = new Vector2(IsMovingLeft() ? -1 : 1, 0);
            }

            SetAnimatorBool(IsMoving());

            if (IsArrived())
            {
                CurrentArrivedCallBack?.Invoke();
                return;
            }
            currentMove?.UpdatePath();
        }

        private bool IsMoving()
        {
            return IsActionActive && !IsArrived();
        }

        public bool IsArrived()
        {
            return (currentMove.data.destination - rb.position).magnitude < destinationOffset;
        }
    }

    public static class MoveFactory
    {
        public static MoveStrategy CreateMove(MoveData data)
        {
            switch (data.GetMovementType())
            {
                case MovementType.Linearity:
                    return new MoveLinearity(data);
                case MovementType.Lerp:
                    return new LerpMovement(data);
                default:
                    throw new UnityException("Invalid moveStrategy");
            }
        }
    }

    public abstract class MoveStrategy
    {
        public MoveData data;

        public MoveStrategy(MoveData data)
        {
            this.data = data;
        }

        public void SetDestination(Vector2 destination)
        {
            data.destination = destination;
        }

        public abstract void UpdatePath();
    }

    public class MoveLinearity : MoveStrategy
    {
        public MoveLinearity(MoveData data) : base(data) { }

        public override void UpdatePath()
        {
            Vector2 direction = (data.destination - data.rb.position).normalized;
            data.rb.MovePosition(data.rb.position + data.Speed * Time.deltaTime * direction);
        }
    }

    public class LerpMovement : MoveStrategy
    {
        public LerpMovement(MoveData data) : base(data) { }

        public override void UpdatePath()
        {
            Vector2 direction = (data.destination - data.rb.position).normalized;
            data.rb.MovePosition(
                Vector2.Lerp(data.rb.position, data.rb.position + direction * data.Speed, 
                data.TiltSpeed * Time.deltaTime)
            );
        }
    }

    [System.Serializable]
    public class MoveData
    {
        [SerializeField] MovementType movementType = MovementType.Linearity;
        [SerializeField] float speed = 10f;
        [Conditional("movementType", MovementType.Lerp)]
        [SerializeField] float tiltSpeed = 0.1f;

        [HideInInspector]
        public Vector2 destination = new Vector2();
        [HideInInspector]
        public Rigidbody2D rb;

        public MovementType GetMovementType()
        {
            return movementType;
        }

        public float Speed { get => speed; }
        public float TiltSpeed
        {
            get
            {
                if(movementType != MovementType.Lerp)
                {
                    throw new UnityException("TiltSpeed is not comparable with " + movementType);
                }
                return tiltSpeed;
            }
        }

        public Vector2 GetMoveDirection()
        {
            return (destination - rb.position).normalized;
        }
    }
}