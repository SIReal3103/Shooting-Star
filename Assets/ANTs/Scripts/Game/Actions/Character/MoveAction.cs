using ANTs.Template;
using System;
using UnityEngine;
namespace ANTs.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveAction : ActionBase
    {
        [SerializeField] bool facingWithDirection = true;
        [SerializeField] float timeBetweenChangeFacingDirection = 0.5f;
        [Conditional("facingWithDirection", true)]
        [SerializeField] Transform model;
        [Space(10)]
        [SerializeField] MoveData initialMoveData;
        [Space(5)]
        [SerializeField] float destinationOffset = 0.5f;

        private Rigidbody2D rb;
        private MoveStrategy currentMove;
        private Action CurrentArrivedCallBack;
        private float timeSinceLastChangeFacingDirection;

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
            currentMove = MoveFactory.CreateMove(data, rb);
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
            base.ActionFixedUpdate();

            SetAnimatorBool(IsMoving());

            if (IsArrived())
            {
                CurrentArrivedCallBack?.Invoke();
                return;
            }

            if (facingWithDirection && timeBetweenChangeFacingDirection < timeSinceLastChangeFacingDirection)
            {
                model.right = new Vector2(IsMovingLeft() ? -1 : 1, 0);
                timeSinceLastChangeFacingDirection = 0f;
            }

            timeSinceLastChangeFacingDirection += Time.deltaTime;

            currentMove?.UpdatePath();
        }

        private bool IsMoving()
        {
            return IsActionActive && !IsArrived();
        }

        public bool IsArrived()
        {
            return (currentMove.data.destination - rb.position).sqrMagnitude < destinationOffset * destinationOffset;
        }
    }

    public static class MoveFactory
    {
        public static MoveStrategy CreateMove(MoveData data, Rigidbody2D rb)
        {
            switch (data.GetMovementType())
            {
                case MovementType.Linearity:
                    return new MoveLinearity(data, rb);
                case MovementType.Lerp:
                    return new LerpMovement(data, rb);
                case MovementType.Smooth:
                    return new SmoothMovement(data, rb);
                default:
                    throw new UnityException("Invalid moveStrategy");
            }
        }
    }

    public abstract class MoveStrategy
    {
        public MoveData data;
        public Rigidbody2D rb;

        public MoveStrategy(MoveData data, Rigidbody2D rb)
        {
            this.data = data;
            this.rb = rb;
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
        public MoveLinearity(MoveData data, Rigidbody2D rb) : base(data, rb) { }

        public override void UpdatePath()
        {
            data.direction = (data.destination - rb.position).normalized;
            rb.MovePosition(rb.position + data.Speed * Time.deltaTime * data.direction);
        }
    }

    public class LerpMovement : MoveStrategy
    {
        public LerpMovement(MoveData data, Rigidbody2D rb) : base(data, rb) { }

        public override void UpdatePath()
        {
            data.direction = (data.destination - rb.position).normalized;
            rb.MovePosition(Vector2.Lerp(rb.position, data.destination, data.TiltSpeed * Time.deltaTime));
        }
    }

    public class SmoothMovement : MoveStrategy
    {
        public SmoothMovement(MoveData data, Rigidbody2D rb) : base(data, rb) { }

        public override void OnDirectionUpdated()
        {
            base.OnDirectionUpdated();
            rb.velocity = Vector2.Lerp(rb.velocity, data.direction.normalized * data.Speed, data.TiltSpeed * Time.deltaTime);
        }

        public override void UpdatePath()
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, data.TiltSpeed * Time.deltaTime);
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
        [SerializeField] float tiltSpeed = 2f;

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

        public Vector2 GetMoveDirection()
        {
            return direction.normalized;
        }
    }
}