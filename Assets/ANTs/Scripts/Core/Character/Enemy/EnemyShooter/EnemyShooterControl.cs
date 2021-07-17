﻿using Panda;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(WeaponHandler))]
    [RequireComponent(typeof(MoveAction))]
    public class EnemyShooterControl : MonoBehaviour
    {
        [SerializeField] ANTsPath path;

        private WeaponHandler weaponHandler;
        private Transform playerTransform;
        private MoveAction move;
        private bool isArrived;

        private void Awake()
        {
            move = GetComponent<MoveAction>();
            weaponHandler = GetComponent<WeaponHandler>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnEnable()
        {
            move.OnArrivedEvent += OnArrived;
        }

        private void OnDisable()
        {
            move.OnArrivedEvent -= OnArrived;
        }

        [Task]
        public void ProgressNextPosition()
        {
            if (Task.current.isStarting)
            {
                move.StartMovingTo(path.GetPosition());
                isArrived = false;
                path.Progress();
            }
            if (isArrived) Task.current.Succeed();
        }

        [Task]
        public void ShootAtPlayer()
        {
            if (Task.current.isStarting)
            {
                weaponHandler.DirectWeaponAttachmentTo(playerTransform.position);
                weaponHandler.TriggerProjectileWeapon();
                Task.current.Succeed();
            }
        }

        public void OnArrived()
        {
            isArrived = true;
        }
    }
}