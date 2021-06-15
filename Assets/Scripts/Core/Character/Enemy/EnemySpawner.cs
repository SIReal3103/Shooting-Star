using System;

using UnityEngine;

namespace Game.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] EnemyPool pool;
        [SerializeField] float spawnRate = 1f;

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
                SpawnEnemyAtPoint(GetRandomSpawnPoint());
                timeSinceLastSpawn = 0;
            }
        }

        private void SpawnEnemyAtPoint(Vector2 spawnPoint)
        {
            pool.Pop().InitData(new EnemyData(spawnPoint));
        }

        struct Edge
        {
            public Vector2 a;
            public Vector2 b;
        }

        Vector2 GetRandomSpawnPoint()
        {
            Edge edge = GetRandomSpawnEdge();
            return GetRandomPointBetween(edge);
        }

        private Edge GetRandomSpawnEdge()
        {
            int a = RandomIntRange(0, transform.childCount);
            int b = GetNextChildIndex(a);
            return new Edge { a = GetPositionOfChild(a), b = GetPositionOfChild(b) };
        }

        private Vector2 GetRandomPointBetween(Edge edge)
        {
            Vector2 delt = edge.b - edge.a;
            return edge.a + delt * RandomBetween01();
        }
        
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
                Gizmos.DrawLine(GetPositionOfChild(i), GetPositionOfChild(GetNextChildIndex(i)));
        }

        private int GetNextChildIndex(int i)
        {
            return (i + 1) % transform.childCount;
        }

        private Vector3 GetPositionOfChild(int i)
        {
            return transform.GetChild(i).position;
        }

        private int RandomIntRange(int left, int right)
        {
            return UnityEngine.Random.Range(left, right);
        }

        private float RandomBetween01()
        {
            return UnityEngine.Random.value;
        }
    }
}