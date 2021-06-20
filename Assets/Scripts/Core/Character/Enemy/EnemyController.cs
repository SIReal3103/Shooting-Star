using UnityEngine;

using ANTs.Template;

namespace ANTs.Game
{
    [RequireComponent(typeof(EnemyBehaviour))]
    [RequireComponent(typeof(Damageable))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] ANTsPolygon PlayGround;

        EnemyBehaviour enemy;

        private void Start()
        {
            enemy = GetComponent<EnemyBehaviour>();
        }

        private void Update()
        {
            if (GetComponent<Damageable>().IsDead())
            {
                enemy.Dead();
            }
        }
    }
}