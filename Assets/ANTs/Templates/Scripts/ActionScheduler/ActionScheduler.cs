using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ANTs.Template
{
    public class ActionScheduler : MonoBehaviour
    {
        public bool[,] actionMask = new bool[100, 100];

        private List<ActionBase>[] cancelActions = new List<ActionBase>[100];
        private Dictionary<ActionBase, int> getId = new Dictionary<ActionBase, int>();

        private void Awake()
        {
            ActionBase[] actions = GetComponents<ActionBase>();

            for(int i = 0; i < actions.Length; i++)
            {
                getId[actions[i]] = i;

                cancelActions[i] = new List<ActionBase>();
                for(int j = 0; j < actions.Length; j++)
                {
                    if(actionMask[i, j])
                        cancelActions[i].Add(actions[j]);
                }
            }
        }

        internal void Trigger(ActionBase actionBase)
        {
            foreach(ActionBase action in cancelActions[getId[actionBase]])
            {
                Debug.Log(action.GetType().Name);
            }
        }
    }
}