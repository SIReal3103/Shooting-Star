using System;
using UnityEngine;
using ANTs.Template;
using MoreMountains.Tools;
namespace ANTs.Core
{
    public enum MovementType
    {
        Linearity,
        Lerp
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        public event Action OnArrivedEvent;

        [SerializeField] MovementType movement;
        [SerializeField] MoveData initialMoveData;
        [SerializeField] float destinationOffset = 0.1f;

        private MoveData currentMoveData;
        private Rigidbody2D rb;
        private MoveStrategy moveStrategy;
        private bool isStop = true;

        #region ACCESSORS
        public bool IsMoving()
        {
            return !isStop;
        }

        public Vector2 GetMoveDirection()
        {
            return moveStrategy.data.GetMoveDirection();
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
        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            LoadMoveStrategy(MoveFactory.CreateMove(movement));
        }

        private void Update()
        {
            if (isStop) return;

            moveStrategy?.UpdatePath();
            if (IsArrived())
            {
                OnArrivedEvent?.Invoke();
                StopMoving();
            }
        }

        private void LoadMoveStrategy(MoveStrategy moveStrategy)
        {
            this.moveStrategy = moveStrategy;
            LoadMoveData(initialMoveData);
        }

        private bool IsArrived()
        {
            return (moveStrategy.data.destination - rb.position).magnitude < destinationOffset;
        }

        public void LoadMoveData(MoveData data)
        {
            currentMoveData = data;

            moveStrategy.data = data;
            moveStrategy.data.rb = this.rb;
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