using UnityEngine;

namespace Game.Core
{
    public class EnemyPool : ANTsPool<EnemyPool, EnemyBehaviour>
    {

    }

    public class EnemyData
    {
        public Vector3 spawnPosition;

        public EnemyData(Vector3 position)
        {
            this.spawnPosition = position;
        }
    }
}