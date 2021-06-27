using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{

    public class ActionScheduler : MonoBehaviour
    {
        public const int MAX_MASK = 100;

        [SerializeField] bool[] maskTable;
        [Tooltip("Default animator (in model) if null")]
        [SerializeField] Animator animator;

        private Dictionary<IAction, int> getMaskId = new Dictionary<IAction, int>();
        private IAction[] actions;

        private void Awake()
        {
            actions = GetComponents<IAction>();
            animator = GetComponentInChildren<Animator>();

            foreach (ActionBase action in actions)
                action.animator = animator;

            for (int i = 0; i < actions.Length; i++)
                getMaskId.Add(actions[i], i);
        }


        public void StopActionRelavetiveTo(IAction action)
        {
            int startAction = getMaskId[action];
            for (int stopAction = 0; stopAction < actions.Length; stopAction++)
            {
                if (maskTable[startAction * actions.Length + stopAction])
                    actions[stopAction].ActionStop();
            }
        }
    }
}