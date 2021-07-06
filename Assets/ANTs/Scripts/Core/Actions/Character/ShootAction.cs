using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(WeaponHandler))]
    public class ShootAction : ActionBase
    {
        [SerializeField] bool isAutoFire = false;
        [Conditional("isAutoFire")]
        [SerializeField]
        float timeBetweenFire = 0.5f;

        private float timeSinceLastFire = Mathf.Infinity;

        public override void ActionStart()
        {
            base.ActionStart();
            if (!isAutoFire)
            {
                FireBehaviour();
                ActionStop();
            }
        }

        protected override void ActionUpdate()
        {
            base.ActionUpdate();
            if (isAutoFire)
            {
                if (timeSinceLastFire > timeBetweenFire)
                {
                    FireBehaviour();
                    timeSinceLastFire = 0;
                }

                UpdateTimer();
            }
        }

        private void FireBehaviour()
        {
            GetComponent<WeaponHandler>().TriggerProjectileWeapon();
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }
    }
}
