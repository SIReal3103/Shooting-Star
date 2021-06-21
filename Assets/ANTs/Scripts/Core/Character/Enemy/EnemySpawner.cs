using System;
using UnityEngine;

using ANTs.Template;

namespace ANTs.Game
{
    [RequireComponent(typeof(EnemyPool))]
    public class EnemySpawner : MonoBehaviour
    {
        [Tooltip("The path which the enemy spawn on")]
        [SerializeField] ANTsPolygon spawnPath;
        [Tooltip("Time between spawns")]
        [SerializeField] float spawnRate = 1f;

        private EnemyPool enemyPool;
        private float timeSinceLastSpawn = Mathf.Infinity;

        private void Start()
        {
            enemyPool = GetComponent<EnemyPool>();
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