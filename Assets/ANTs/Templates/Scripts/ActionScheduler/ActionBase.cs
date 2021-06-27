﻿using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace ANTs.Template
{
    [RequireComponent(typeof(ActionScheduler))]
    public abstract class ActionBase : MonoBehaviour, IAction
    {
        public event Action OnActionStartEvent;
        public event Action OnActionStopEvent;

        [Serializable]
        enum TransitionMode {
            Trigger,
            Boolean,
            Default
        }        

        [SerializeField] bool actionStartOnPlay = false;
        [ReadOnly]
        [SerializeField] bool isActionStart = false;
        [SerializeField] TransitionMode transitionMode = TransitionMode.Default;

        [HideInInspector]
        protected AnimatorEvents animatorEvents;
        protected Animator animator;
        private ActionScheduler scheduler;

        public bool IsActionStart { get => isActionStart; }
       
        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            animatorEvents = GetComponentInChildren<AnimatorEvents>();
            scheduler = GetComponent<ActionScheduler>();

            if (transitionMode == TransitionMode.Default)
                transitionMode = GetDefaultTriggerMode(GetType().Name);
        }

        protected virtual void Start()
        {
            if (actionStartOnPlay)
                ActionStart();
        }

        protected void Update()
        {
            if (IsActionStart == true)
            {
                ActionUpdate();
            }
        }

        private TransitionMode GetDefaultTriggerMode(string name)
        {
            switch (name)
            {
                case "MoveAction":
                    return TransitionMode.Boolean;
                default:
                    return TransitionMode.Trigger;
            }
        }        

        public virtual void ActionStart()
        {
            if (scheduler.IsActionPrevent(this)) return;

            isActionStart = true;
            OnActionStartEvent?.Invoke();

            if(transitionMode == TransitionMode.Trigger) SetTrigger();

            scheduler.StopActionRelavetiveTo(this);
        }

        public virtual void ActionStop()
        {
            isActionStart = false;
            OnActionStopEvent?.Invoke();
        }

        protected virtual void ActionUpdate() { }

        #region ===================================Animator control
        protected void SetAnimationBool(bool value)
        {
            Assert.AreEqual(transitionMode, TransitionMode.Boolean);
            if (animator) animator.SetBool("Is" + GetType().Name, value);
        }

        private void SetTrigger()
        {
            Assert.AreEqual(transitionMode, TransitionMode.Trigger);
            if (animator) animator.SetTrigger(GetType().Name);
        }
        #endregion
    }
}