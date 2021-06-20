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
        [SerializeField] MoveData initialData;

        private bool isStop = true;
        private MoveStrategy moveStrategy;

        public MoveStrategy MoveStrategy
        {
            get => moveStrategy;

            set
            {
                moveStrategy = value;
                LoadDataToStrategy();
            }
        }

        private void Start()
        {
            initialData.rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (isStop) return;
            MoveStrategy?.UpdatePath();
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

        public static MoveStrategy CreateMove(MovementType movetype)
        {
            switch (movetype)
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
            data.rb.MovePosition(data.rb.position + data.speed * Time.deltaTime * direction);
        }
    }

    public class LerpMovement : MoveStrategy
    {
        public override void UpdatePath()
        {
            data.rb.MovePosition(Vector2.Lerp(data.rb.position, data.destination, data.tiltSpeed * Time.deltaTime));
        }
    }

    [System.Serializable]
    public class MoveData
    {
        public float speed = 10f;
        public float tiltSpeed;
        [HideInInspector]
        public Vector2 destination;
        [HideInInspector]
        public Rigidbody2D rb;
    }
}