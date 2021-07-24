using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ANTs.UI
{
    public class UpgradeCellDisplayer : MonoBehaviour
    {        
        public void Setup(Sprite icon)
        {
            GetComponentInChildren<Image>().sprite = icon;
        }
    }
}