using ANTs.Template;
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
        [SerializeField] MoveData initialMoveData;
        [SerializeField] float destinationOffset = 0.5f;

        private Rigidbody2D rb;
        private MoveStrategy currentMove;
        private float timeSinceLastChangeFacingDirection;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            SetMoveData(initialMoveData);
        }

        public void StartMovingTo(Vector2 destination)
        {
            ActionStart();
            currentMove.Destination = destination;
        }

        public Vector2 GetMovingDirection()
        {
            return currentMove.GetDirection();
        }

        public void ChangeFacingDirection(Vector2 position)
        {
            model.right = new Vector2((
                ((Vector2)transform.position - position).x > 0) ? -1 : 1, 0
            );
        }

        public void SetMoveData(MoveData data)
        {
            currentMove = MoveFactory.CreateMove(data, rb);
        }

        private bool IsMovingLeft()
        {
            return GetMovingDirection().x < 0f;
        }

        protected override void ActionFixedUpdate()
        {
            base.ActionFixedUpdate();

            SetAnimatorBool(IsMoving());

            if (IsArrived())
            {
                currentMove.Stop();
                return;
            }

            if (facingWithDirection && timeBetweenChangeFacingDirection < timeSinceLastChangeFacingDirection)
            {
                UpdateFacingDirection();
            }

            currentMove?.UpdatePath();
            timeSinceLastChangeFacingDirection += Time.deltaTime;
        }

        private void UpdateFacingDirection()
        {
            model.right = new Vector2(IsMovingLeft() ? -1 : 1, 0);
            timeSinceLastChangeFacingDirection = 0f;
        }

        private bool IsMoving()
        {
            return IsActionActive && !IsArrived();
        }

        public bool IsArrived()
        {
            return currentMove.IsArrived(destinationOffset);
        }
    }

    public static class MoveFactory
    {
        public static MoveStrategy CreateMove(MoveData data, Rigidbody2D rb)
        {
            switch (data.GetMovementType())
            {
                case MovementType.Linearity:
                    return new DynamicLinearity(data, rb);
                case MovementType.Smooth:
                    return new DynamicSmooth(data, rb);
                default:
                    throw new UnityException("Invalid moveStrategy");
            }
        }
    }

    public abstract class MoveStrategy
    {
        public MoveData data;
        public Rigidbody2D rb;
        private Vector2 destination = Vector2.one * 10000;

        public Vector2 Destination { get => destination; set { destination = value; OnDestinationUpdated(); } }

        public MoveStrategy(MoveData data, Rigidbody2D rb)
        {
            this.data = data;
            this.rb = rb;
        }

        public bool IsArrived(float destinationOffset)
        {
            return (Destination - rb.position).sqrMagnitude < destinationOffset * destinationOffset;
        }

        public Vector2 GetDirection()
        {
            return (Destination - rb.position).normalized;
        }

        public void Stop()
        {
            rb.velocity = Vector2.zero;
        }

        public virtual void OnDestinationUpdated() { }
        public abstract void UpdatePath();
    }

    public class DynamicLinearity : MoveStrategy
    {
        public DynamicLinearity(MoveData data, Rigidbody2D rb) : base(data, rb) { }

        public override void UpdatePath()
        {
            rb.velocity = GetDirection() * data.MaxSpeed;
        }
    }

    public class DynamicSmooth : MoveStrategy
    {
        public DynamicSmooth(MoveData data, Rigidbody2D rb) : base(data, rb) { }
        private float currentVelocityMagnitude = 0f;

        public float CurrentVelocityMagnitude { get => currentVelocityMagnitude; set => currentVelocityMagnitude = Mathf.Clamp(value, 0f, data.MaxSpeed); }

        public override void OnDestinationUpdated()
        {
            base.OnDestinationUpdated();
            CurrentVelocityMagnitude += data.Acceleration * Time.deltaTime;
        }

        public override void UpdatePath()
        {
            CurrentVelocityMagnitude -= data.Deacceleration * Time.deltaTime;
            SetRigidbody2DVelocity(CurrentVelocityMagnitude);
        }

        private void SetRigidbody2DVelocity(float velocityMagnitude)
        {
            rb.velocity = velocityMagnitude * GetDirection();
        }
    }

    public enum MovementType
    {
        Linearity,
        Smooth
    }

    [System.Serializable]
    public class MoveData
    {
        [SerializeField] MovementType movementType = MovementType.Linearity;
        [Conditional("movementType", MovementType.Linearity, MovementType.Smooth)]
        [SerializeField] float maxSpeed = 10f;
        [Conditional("movementType", MovementType.Smooth)]
        [SerializeField] float acceleration = 50f;
        [Conditional("movementType", MovementType.Smooth)]
        [SerializeField] float deacceleration = 50f;

        public MovementType GetMovementType()
        {
            return movementType;
        }

        public float MaxSpeed { get => maxSpeed; }
        public float Acceleration { get => acceleration; }
        public float Deacceleration { get => deacceleration; }
    }
}