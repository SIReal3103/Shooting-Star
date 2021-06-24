using System;
using UnityEngine;

using ANTs.Template;

namespace ANTs.Core
{
    //TODO: Enemy not die

    [RequireComponent(typeof(Mover))]
    public class EnemyFacde : MonoBehaviour, IANTsPoolable<EnemyPool, EnemyFacde>
    {
        public event Action OnArrivedEvent
        {
            add { GetComponent<Mover>().OnArrivedEvent += value; }
            remove { GetComponent<Mover>().OnArrivedEvent -= value; }
        }

        private Mover mover;

        public EnemyPool CurrentPool { get; set; }
        
        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        #region BEHAVIOURS
        public void StartMovingTo(Vector2 destination)
        {
            mover.StartMovingTo(destination);
        }
        public void StopMoving()
        {
            mover.StopMoving();
        }
        public void Dead()
        {
            ReturnToPool();
        }
        public void LoadMoveData(MoveData data)
        {
            mover.LoadMoveData(data);
        }
        #endregion

        #region IANTsPoolObject IMPLEMENTATION
        public void ReturnToPool()
        {
            CurrentPool.ReturnToPool(this);
        }
        public void WakeUp(object args)
        {
            EnemyData data = args as EnemyData;
            gameObject.SetActive(true);
            transform.position = data.spawnPosition;
        }
        public void Sleep()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}

