using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(ANTsPool))]
    public class EnemySpawner : MonoBehaviour
    {
        [Tooltip("The path which the enemy spawn on")]
        [SerializeField] ANTsPolygon spawnPath;
        [Tooltip("Time between spawns")]
        [SerializeField] float spawnRate = 1f;

        private ANTsPool enemyPool;
        private float timeSinceLastSpawn = Mathf.Infinity;

        private void Awake()
        {
            enemyPool = GetComponent<ANTsPool>();
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
            enemyPool.Pop(new EnemyData(spawnPoint));
        }
    }
}