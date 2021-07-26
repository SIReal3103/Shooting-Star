using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ANTs.Template.UI
{
    public class DebugDisplayer : MonoBehaviour
    {
        class TextIEnumerator : IEnumerator
        {
            Transform rootInfo;
            GameObject infoPrefab;
            List<Text> texts = new List<Text>();
            int position = -1;

            public object Current => texts[position];

            public TextIEnumerator(Transform rootInfo, GameObject infoPrefab)
            {
                this.rootInfo = rootInfo;
                this.infoPrefab = infoPrefab;
            }

            public bool MoveNext()
            {                
                if (++position >= texts.Count)
                {      
                    texts.Add(Instantiate(infoPrefab, rootInfo).GetComponentInChildren<Text>());
                }
                return true;
            }

            public void Reset()
            {
                position = -1;
            }
        }

        static public List<DebugDisplayer> instances;

        [SerializeField] Transform rootInfo;
        [SerializeField] GameObject infoPrefab;

        List<IDisplayOnHUD> displayees;
        TextIEnumerator textPool = null;

        private void Start()
        {
            ResetUI();
        }

        private void Update()
        {
            UpdateUI();
        }

        public void ResetUI()
        {
            DestroyChildTransforms();
            textPool = new TextIEnumerator(rootInfo, infoPrefab);
            displayees = FindAllDisplayees().ToList();
            UpdateUI();
        }

        public void UpdateUI()
        {
            textPool.Reset();
            foreach(IDisplayOnHUD displayee in displayees)
            {
                foreach (string info in displayee.GetDisplayInfos())
                {
                    if (textPool.MoveNext())
                    {
                        ((Text)textPool.Current).text = info;
                    }
                }
            }                
        }

        private IEnumerable<IDisplayOnHUD> FindAllDisplayees()
        {            
            return FindObjectsOfType<MonoBehaviour>().OfType<IDisplayOnHUD>();
        }

        private void DestroyChildTransforms()
        {
            foreach (Transform child in rootInfo)
            {
                Destroy(child.gameObject);
            }
        }
    }
}