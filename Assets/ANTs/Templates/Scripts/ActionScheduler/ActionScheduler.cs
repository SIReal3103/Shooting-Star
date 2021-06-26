using System;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Template
{

    public class ActionScheduler : MonoBehaviour
    {
        [HideInInspector]
        public SerializableMask maskTable;

        private Dictionary<ActionBase, int> getMaskId = new Dictionary<ActionBase, int>();
        private ActionBase[] actions;

        private void Awake()
        {
            actions = GetComponents<ActionBase>();
            InitializeValues();
        }

        private void InitializeValues()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                getMaskId.Add(actions[i], i);
            }
        }

        public void Trigger(ActionBase actionBase)
        {
            int startAction = getMaskId[actionBase];
            for(int stopAction = 0; stopAction < actions.Length; stopAction++)
            {
                if (maskTable[startAction, stopAction]) actions[stopAction].ActionStop();
            }
        }
    }
}