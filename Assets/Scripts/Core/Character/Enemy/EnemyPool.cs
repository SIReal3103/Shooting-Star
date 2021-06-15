using UnityEngine;

using Game.Template;

namespace Game.Core
{
    public class EnemyPool : Pool<EnemyPool, EnemyObject, EnemyData>
    {

    }


    public class EnemyObject : PoolObject<EnemyPool, EnemyObject, EnemyData>
    {
        public override void InitData(EnemyData data)
        {
            EnemyBehaviour enemy = GetInstance().GetComponent<EnemyBehaviour>();
            AssignData(enemy, data);
        }

        private void AssignData(EnemyBehaviour enemy, EnemyData data)
        {
            enemy.enemyObject = this;

            enemy.transform.position = data.position;
        }
    }

    public class EnemyData
    {
        public Vector3 position;

        public EnemyData(Vector3 position)
        {
            this.position = position;
        }
    }
}