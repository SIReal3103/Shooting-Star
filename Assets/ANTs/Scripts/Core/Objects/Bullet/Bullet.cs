using UnityEngine;

using ANTs.Template;

namespace ANTs.Core
{
    [RequireComponent(typeof(Damager))]
    public class Bullet : Projectile, IANTsPoolable<BulletPool, Bullet>, IProgressable
    {
        [Tooltip("Decide whether if the bullet will be destroyed when out of player view")]
        [SerializeField] bool destroyWhenOutOfScreen = true;
        [Tooltip("The boundaries of which the bullet will be destroyed")]
        [SerializeField] Vector2 outScreenOffSet = Vector2.zero;
        [Space]
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextBulletId;

        public BulletPool CurrentPool { get; set; }
        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextBulletId; }

        private TouchDamager touchDamager;

        protected override void Awake()
        {
            base.Awake();
            touchDamager = GetComponent<TouchDamager>();
        }

        private void Update()
        {
            if (destroyWhenOutOfScreen && CheckIsOutOfScreen())
            {
                ReturnToPool();
            }
        }

        private void OnEnable()
        {
            touchDamager.OnHitEvent += OnHit;
        }
        private void OnDisable()
        {
            touchDamager.OnHitEvent -= OnHit;
        }

        private bool CheckIsOutOfScreen()
        {
            Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
            return
                screenPosition.x < -outScreenOffSet.x || screenPosition.x > 1f + outScreenOffSet.x ||
                screenPosition.y < -outScreenOffSet.y || screenPosition.y > 1f + outScreenOffSet.y;
        }

        void OnHit()
        {
            ReturnToPool();
        }

        public void ReturnToPool() // IANTsPoolable Implementation
        {
            CurrentPool.ReturnToPool(this);
        }

        public void WakeUp(System.Object args) // IANTsPoolable Implementation
        {
            gameObject.SetActive(true);

            BulletData data = args as BulletData;

            transform.position = data.origin;
            SetDirection(data.moveDirection);
            source = data.source;

            touchDamager.source = data.source;
        }

        public void Sleep() // IANTsPoolable Implementation
        {
            gameObject.SetActive(false);
        }
    }
}