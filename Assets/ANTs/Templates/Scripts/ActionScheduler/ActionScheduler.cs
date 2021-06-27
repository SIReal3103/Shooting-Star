using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{

    public class ActionScheduler : MonoBehaviour
    {
        public const int MAX_MASK = 100;

        [SerializeField] bool[] maskTable;
        [Tooltip("Default animator (in model) if null")]

        private Dictionary<IAction, int> getMaskId;
        private IAction[] actions;

        private void Awake()
        {
            getMaskId = new Dictionary<IAction, int>();
            actions = GetComponents<IAction>();

            for (int i = 0; i < actions.Length; i++)
                getMaskId.Add(actions[i], i);
        }

        public bool IsActionPrevent(IAction action)
        {
            return false;
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