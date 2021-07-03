using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(WeaponHandler))]
    public class ShootAction : ActionBase
    {
        [SerializeField] float timeBetweenFire = 0.5f;

        private float timeSinceLastFire = Mathf.Infinity;

        protected override void ActionUpdate()
        {
            FireBehaviour();
            UpdateTimer();
        }

        private void FireBehaviour()
        {
            if (timeSinceLastFire > timeBetweenFire)
            {
                GetComponent<WeaponHandler>().TriggerWeapon();
                timeSinceLastFire = 0;
            }
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }
    }
}
