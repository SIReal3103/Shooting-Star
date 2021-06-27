using ANTs.Template;
using System;
using UnityEngine;
namespace ANTs.Core
{
    public enum MovementType
    {
        Linearity,
        Lerp
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class MoverAction : ActionBase
    {
        public event Action OnArrivedEvent;

        [Header("Facing")]
        [Space(10)]
        [Tooltip("To control actor's facing direction")]
        [SerializeField] Transform model;
        [SerializeField] bool FacingWithDirection;
        [Header("Movement Data")]
        [Space(10)]
        [SerializeField] MovementType movementType;
        [SerializeField] MoveData initialMoveData;
        [SerializeField] float destinationOffset = 0.1f;

        private Rigidbody2D rb;
        private MoveStrategy moveStrategy;

        protected override void Awake()
        {
            base.Awake();

            rb = GetComponent<Rigidbody2D>();
            LoadMoveStrategy(MoveFactory.CreateMove(movementType));
        }

        #region ===========================================Behaviours
        public bool IsMoving()
        {
            return IsActionStart && !IsArrived();
        }

        private bool IsFacingLeft()
        {
            return GetMoveDirection().x < 0f;
        }

        public Vector2 GetMoveDirection()
        {
            return moveStrategy.data.GetMoveDirection();
        }

        public void SetMoveData(MoveData data)
        {
            moveStrategy.data = data;
            moveStrategy.data.rb = rb;
        }
        public void SetDestination(Vector2 destination)
        {
            moveStrategy.data.destination = destination;
        }

        private void LoadMoveStrategy(MoveStrategy moveStrategy)
        {
            this.moveStrategy = moveStrategy;
            SetMoveData(initialMoveData);
        }
        private bool IsArrived()
        {
            return (moveStrategy.data.destination - rb.position).magnitude < destinationOffset;
        }
        #endregion

        #region ============================================ActionBase Implementation
        protected override void ActionUpdate()
        {
            if (FacingWithDirection)
                model.localScale = new Vector2(IsFacingLeft() ? -1 : 1, 1);

            if (IsArrived())
            {
                OnArrivedEvent?.Invoke();
                return;
            }
            moveStrategy?.UpdatePath();
        }
        #endregion
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
                    throw new UnityException("Invalid moveStrategy");
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
            data.rb.MovePosition(Vector2.Lerp(data.rb.position, data.destination, data.Speed * Time.deltaTime));
        }
    }

    [System.Serializable]
    public class MoveData
    {
        [SerializeField] float speed = 10f;

        [HideInInspector]
        public Vector2 destination;
        [HideInInspector]
        public Rigidbody2D rb;

        public float Speed { get => speed; }

        public Vector2 GetMoveDirection()
        {
            return (destination - rb.position).normalized;
        }
    }
}