using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(TouchDamager))]
    public class WeaponAmmo : Projectile, IProgressable, IPoolable
    {
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextBulletId;
        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextBulletId; }

        private TouchDamager touchDamager;
        private Damager damager;

        protected override void Awake()
        {
            base.Awake();
            touchDamager = GetComponent<TouchDamager>();
            damager = GetComponent<Damager>();
        }

        private void OnEnable()
        {
            touchDamager.OnHitEvent += OnHit;
        }
        private void OnDisable()
        {
            touchDamager.OnHitEvent -= OnHit;
        }

        void OnHit()
        {
            gameObject.ReturnToPoolOrDestroy();
        }

        public void WakeUp(object param)
        {
            AmmoData data = (AmmoData)param;
            transform.position = data.spawnPosition;
            StartMovingTo(data.moveDirection);
            touchDamager.SetSource(data.shooter);

            if (data.projectileWeapon.TryGetComponent(out Damager weapon))
            {
                damager.CombineDamageData(weapon.GetDamageData(), weapon.GetDamageCauser());
            }
        }

        public void Sleep() { }
    }

    public class AmmoData
    {
        public GameObject shooter;
        public ProjectileWeapon projectileWeapon;
        public Vector2 spawnPosition;
        public Vector2 moveDirection;

        public AmmoData(GameObject shooter, ProjectileWeapon projectileWeapon, Vector2 spawnPosition, Vector2 moveDirection)
        {
            this.shooter = shooter;
            this.spawnPosition = spawnPosition;
            this.moveDirection = moveDirection;
            this.projectileWeapon = projectileWeapon;
        }
    }
}