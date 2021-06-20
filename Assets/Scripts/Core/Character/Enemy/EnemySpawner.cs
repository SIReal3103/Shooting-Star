using UnityEngine;

using ANTs.Template;

namespace ANTs.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] ANTsPolygon spawnPath;
        [SerializeField] float spawnRate = 1f;

        [SerializeField] EnemyPool pool;

        float timeSinceLastSpawn = Mathf.Infinity;

        private void Start()
        {

        }

        private void Update()
        {
            SpawnEnemyBehaviour();

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timeSinceLastSpawn += Time.deltaTime;
        }

        void SpawnEnemyBehaviour()
        {
            if (timeSinceLastSpawn > spawnRate)
            {
                SpawnEnemyAtPoint(spawnPath.GetRandomPointOnPath());
                timeSinceLastSpawn = 0;
            }
        }

        private void SpawnEnemyAtPoint(Vector2 spawnPoint)
        {
            pool.Pop(new EnemyData(spawnPoint));
        }
    }
}