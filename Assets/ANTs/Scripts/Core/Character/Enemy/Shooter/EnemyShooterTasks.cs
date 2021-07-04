using System.Collections;
using UnityEngine;
using Panda;

namespace ANTs.Core
{
    [RequireComponent(typeof(MoveAction))]
    public class EnemyShooterTasks : MonoBehaviour
    {
        [SerializeField] ANTsPath path;

        private MoveAction move;
        private bool isArrived;

        private void Awake()
        {
            move = GetComponent<MoveAction>();
        }

        private void OnEnable()
        {
            move.OnArrivedEvent += OnArrived;
        }

        private void OnDisable()
        {
            move.OnArrivedEvent -= OnArrived;
        }

        [Task]
        public void ProgressNextPosition()
        {
            if(Task.current.isStarting)
            {
                move.ActionStart();
                move.SetDestination(path.GetPosition());
                isArrived = false;
                path.Progress();                
            }
            if (isArrived) Task.current.Succeed();
        }

        public void OnArrived()
        {
            isArrived = true;
        }
    }
}