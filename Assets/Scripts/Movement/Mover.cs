using UnityEngine;

namespace Game.Movement
{
    public enum MovementType
    {
        Linearity,
        Lerp
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        [SerializeField]
        MoveData data;

        private MoveStategy moveStrategy;
        public MoveStategy MoveStrategy
        {
            get => moveStrategy;

            set
            {
                moveStrategy = value;
                moveStrategy.data = data;
            }
        }

        private void Start() {
            data.rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            MoveStrategy?.UpdatePath();
        }
    }

    public abstract class MoveFactory
    {
        private MoveFactory() { }

        public static MoveStategy CreateMove(MovementType movetype)
        {
            switch(movetype)
            {
                case MovementType.Linearity:
                    return new MoveLinearity();
                case MovementType.Lerp:
                    return new MoveLinearity();
                default:
                    throw new UnityException("Invalid move strategy");
            }
        }
    }

    public abstract class MoveStategy
    {
        public MoveData data;
        public abstract void UpdatePath();
    }

    public class MoveLinearity : MoveStategy
    {
        public override void UpdatePath()
        {
            Vector2 direction = (data.destination - data.rb.position).normalized;
            data.rb.MovePosition(data.rb.position + data.speed * Time.deltaTime * direction);
        }
    }

    public class MoveLerp : MoveStategy
    {
        public override void UpdatePath()
        {
            throw new System.NotImplementedException();
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