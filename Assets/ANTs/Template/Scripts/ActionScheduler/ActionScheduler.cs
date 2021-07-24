using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{

    public class ActionScheduler : MonoBehaviour
    {
        [SerializeField] bool[] maskTable;
        private Dictionary<IAction, int> getMaskId;
        private IAction[] actions;

        private void Awake()
        {
            getMaskId = new Dictionary<IAction, int>();
            actions = GetComponents<IAction>();

            for (int i = 0; i < actions.Length; i++)
                getMaskId.Add(actions[i], i);
        }

        public bool IsPrevented(IAction action)
        {
            int j = getMaskId[action];
            for (int i = 0; i < actions.Length; i++)
            {
                if (GetMask(i, j) && actions[i].IsActionActive)
                    return true;
            }
            return false;
        }

        public void StopActionRelavetiveTo(IAction action)
        {
            int i = getMaskId[action];
            for (int j = 0; j < actions.Length; j++)
            {
                if (GetMask(i, j)) actions[j].ActionStop();
            }
        }

        private bool GetMask(int i, int j)
        {
            return maskTable[i * actions.Length + j];
        }
    }
}