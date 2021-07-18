using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ANTs.Game;
using System.Collections.Generic;

namespace ANTs.UI
{
    public class UpgradeDisplayer : MonoBehaviour
    {
        [SerializeField] Image[] images;
        [SerializeField] Sprite defaultUpgradeIcon;

        Upgrade playerUpgrades;
        private int avaiableSlot = 0;

        private void Awake()
        {
            playerUpgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<Upgrade>();
            avaiableSlot = images.Length;
        }

        private void OnEnable()
        {
            playerUpgrades.OnUpgradeUpdateEvent += OnUpgradeUpdate;
        }

        private void OnDisable()
        {
            playerUpgrades.OnUpgradeUpdateEvent -= OnUpgradeUpdate;
        }

        private void Update()
        {
            
        }

        public void OnUpgradeUpdate()
        {
            List<UpgradeBase> upgrades = playerUpgrades.GetUpgrades();
            if (upgrades.Count > avaiableSlot) Debug.LogWarning("The number of upgrade is exceed number of slot!");
            for(int i = 0; i < avaiableSlot; i++)
            {
                if(i < upgrades.Count)
                {
                    images[i].sprite = upgrades[i].icon;
                }
                else
                {
                    images[i].sprite = defaultUpgradeIcon;
                }
            }
        }
    }
}