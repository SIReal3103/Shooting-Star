using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class EnemyPool : ANTsPool
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