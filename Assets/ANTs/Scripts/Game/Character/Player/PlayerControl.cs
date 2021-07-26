using ANTs.Template.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(MoveAction))]
    [RequireComponent(typeof(WeaponHandler))]
    [RequireComponent(typeof(Damageable))]
    public class PlayerControl : MonoBehaviour
    {
        private MoveAction mover;

        private void Awake()
        {
            mover = GetComponent<MoveAction>();
        }

        private void OnEnable()
        {
            GetComponent<Damageable>().OnActorDieEvent += GetComponent<DieAction>().ActionStart;
        }

        private void OnDisable()
        {
            GetComponent<Damageable>().OnActorDieEvent -= GetComponent<DieAction>().ActionStart;
        }

        public void StartMovingWith(Vector2 direction)
        {            
            mover.StartMovingTo((Vector2)transform.position + direction.normalized);
        }

        public void DirectWeaponTo(Vector2 position)
        {
            GetComponent<WeaponHandler>().DirectWeaponAttachmentTo(position);
        }

        public void UpgradeWeapon()
        {
            GetComponent<WeaponHandler>().UpgradeProjectileWeapon();
        }

        public void ChangeStrongerBullet()
        {
            GetComponent<WeaponHandler>().UpgradeCurrentAmmo();
        }
    }
}