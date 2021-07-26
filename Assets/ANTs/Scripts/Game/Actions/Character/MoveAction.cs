using ANTs.Template;
using System;
using UnityEngine;
namespace ANTs.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveAction : ActionBase
    {
        [SerializeField] bool FacingWithDirection;
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
            currentMove.SetDestination(destination);
            CurrentArrivedCallBack?.Invoke();
            CurrentArrivedCallBack = OnArrivedCallBack;
        }
        
        public void StartMovingWith(Vector2 destination)
        {
            ActionStart();
            currentMove.SetDirection(destination);
            CurrentArrivedCallBack = null;
        }

        public Vector2 GetMoveDirection()
        {
            return currentMove.data.GetMoveDirection();
        }

        public void FacingTo(Vector2 position)
        {
            model.right = new Vector2((
                ((Vector2)transform.position - position).x > 0) ? -1 : 1, 0
            );
        }

        public void SetMoveData(MoveData data)
        {
            if (data.rb == null) data.rb = rb;
            currentMove = MoveFactory.CreateMove(data);
        }

        public void SetVelocity(Vector2 velocity)
        {
            currentMove.SetDirection(velocity);
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
                case MovementType.Smooth:
                    return new SmoothMovement(data);
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

        public void SetDirection(Vector2 velocity)
        {
            data.direction = velocity;
            OnDirectionUpdated();
        }

        public virtual void OnDirectionUpdated() { }
        public abstract void UpdatePath();
    }

    public class MoveLinearity : MoveStrategy
    {
        public MoveLinearity(MoveData data) : base(data) { }

        public override void UpdatePath()
        {
            data.direction = (data.destination - data.rb.position).normalized;
            data.rb.MovePosition(data.rb.position + data.Speed * Time.deltaTime * data.direction);
        }
    }

    public class LerpMovement : MoveStrategy
    {
        public LerpMovement(MoveData data) : base(data) { }

        public override void UpdatePath()
        {
            data.direction = (data.destination - data.rb.position).normalized;
            data.rb.MovePosition(Vector2.Lerp(data.rb.position, data.destination, data.TiltSpeed * Time.deltaTime));
        }
    }

    public class SmoothMovement : MoveStrategy
    {
        public SmoothMovement(MoveData data) : base(data) { }

        public override void OnDirectionUpdated()
        {
            base.OnDirectionUpdated();
            data.rb.velocity = Vector2.Lerp(data.rb.velocity, data.direction.normalized * data.Speed, data.TiltSpeed * Time.deltaTime);
        }

        public override void UpdatePath()
        {
            Debug.Log(data.TiltSpeed);
            data.rb.velocity = Vector2.Lerp(data.rb.velocity, Vector2.zero, data.TiltSpeed * Time.deltaTime);
        }
    }

    public enum MovementType
    {
        Linearity,
        Lerp,
        Smooth
    }

    [System.Serializable]
    public class MoveData
    {
        [SerializeField] MovementType movementType = MovementType.Linearity;
        [Conditional("movementType", MovementType.Linearity, MovementType.Smooth)]
        [SerializeField] float speed = 10f;
        [Conditional("movementType", MovementType.Lerp, MovementType.Smooth)]
        [SerializeField] float tiltSpeed = 0.1f;
        [SerializeField] public Rigidbody2D rigidbody2D = null;

        [HideInInspector]
        public Vector2 destination = Vector2.positiveInfinity;
        [HideInInspector]
        public Vector2 direction = Vector2.positiveInfinity;

        public MovementType GetMovementType()
        {
            return movementType;
        }

        public float Speed { get => speed; }
        public float TiltSpeed
        {
            get
            {
                if (movementType != MovementType.Lerp && movementType != MovementType.Smooth)
                {
                    throw new UnityException("TiltSpeed is not comparable with " + movementType);
                }
                return tiltSpeed;
            }
        }
        public Rigidbody2D rb { get => rigidbody2D; set { rigidbody2D = value; } }

        public Vector2 GetMoveDirection()
        {
            return direction.normalized;
        }
    }
}