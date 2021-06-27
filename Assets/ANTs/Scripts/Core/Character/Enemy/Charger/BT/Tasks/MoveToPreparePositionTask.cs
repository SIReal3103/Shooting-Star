using ANTs.Template;
using Panda;
using UnityEngine;

namespace ANTs.Core
{
    public class MoveToPreparePositionTask : MonoBehaviour
    {
        [SerializeField] ANTsPolygon prepareZone;

        private Vector2 preparePosition;
        private EnemyNinjaFacade charger;
        private bool isArrived;

        public void Awake()
        {
            charger = GetComponent<EnemyNinjaFacade>();
        }

        public void OnEnable()
        {
            charger.OnArrivedEvent += OnMoverArrived;
        }

        private void OnDisable()
        {
            charger.OnArrivedEvent -= OnMoverArrived;
        }

        private void Start()
        {
            preparePosition = prepareZone.GetRandomPointOnSurface();
        }

        [Task]
        public void MoveToPreparePosition()
        {
            if (Task.current.isStarting)
            {
                isArrived = false;
                charger.MoveTo(preparePosition);
            }

            if (isArrived) Task.current.Succeed();
        }

        public void OnMoverArrived()
        {
            isArrived = true;
        }
    }
}