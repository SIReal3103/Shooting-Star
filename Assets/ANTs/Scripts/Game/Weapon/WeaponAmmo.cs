using ANTs.Template;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(TouchDamager))]
    public class WeaponAmmo : Projectile, IProgressable, IPoolable
    {
        #region ==================================SerializeField

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
            transform.position = data.origin;
            SetDirection(data.moveDirection);
            touchDamager.SetSource(data.shooter);
            if (data.weaponFireFrom.TryGetComponent(out Damager weaponDamager))
            {
                GetComponent<Damager>().AddToDamageData(weaponDamager.GetDamageData());
            }
        }

        public void Sleep() { }
    }

    public class AmmoData
    {
        public GameObject shooter;
        public ProjectileWeapon weaponFireFrom;
        public Vector2 origin;
        public Vector2 moveDirection;

        public AmmoData(GameObject shooter, ProjectileWeapon weaponFireFrom, Vector2 origin, Vector2 moveDirection)
        {
            this.shooter = shooter;
            this.origin = origin;
            this.moveDirection = moveDirection;
            this.weaponFireFrom = weaponFireFrom;
        }
    }
}