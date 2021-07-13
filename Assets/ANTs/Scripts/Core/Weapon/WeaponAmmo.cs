using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(TouchDamager))]
    public class WeaponAmmo : Projectile, IProgressable
    {
        #region ==================================SerializeField

        [Tooltip("Decide whether if the bullet will be destroyed when out of player view")]
        [SerializeField] bool destroyWhenOutOfScreen = true;
        [Tooltip("The boundaries of which the bullet will be destroyed")]
        [SerializeField] Vector2 outScreenOffSet = Vector2.zero;
        [Space]
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextBulletId;

        #endregion
        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextBulletId; }

        private TouchDamager touchDamager;

        protected override void Awake()
        {
            base.Awake();
            touchDamager = GetComponent<TouchDamager>();

            gameObject.SetWakeUpDelegate(args =>
            {
                AmmoData data = (AmmoData)args;
                transform.position = data.origin;
                SetDirection(data.moveDirection);
                touchDamager.Source = data.source;
            });
        }

        private void Update()
        {
            if (destroyWhenOutOfScreen && IsOutOfScreen())
            {
                gameObject.ReturnToPoolOrDestroy();
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

        private bool IsOutOfScreen()
        {
            Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
            return
                screenPosition.x < -outScreenOffSet.x || screenPosition.x > 1f + outScreenOffSet.x ||
                screenPosition.y < -outScreenOffSet.y || screenPosition.y > 1f + outScreenOffSet.y;
        }

        void OnHit()
        {
            gameObject.ReturnToPoolOrDestroy();
        }
    }

    public class AmmoData
    {
        public GameObject source;
        public Vector2 origin;
        public Vector2 moveDirection;

        public AmmoData(GameObject source, Vector2 origin, Vector2 moveDirection)
        {
            this.source = source;
            this.origin = origin;
            this.moveDirection = moveDirection;
        }
    }
}