using ANTs.Game;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.UI
{
    public class UpgradeGridDisplayer : MonoBehaviour
    {
        [SerializeField] UpgradeCellDisplayer cellPrefab;

        Upgrade playerUpgrades;
        private int avaiableSlot = 0;

        private void Awake()
        {
            playerUpgrades = GameObject.FindGameObjectWithTag("Player").GetComponent<Upgrade>();
        }

        private void OnEnable()
        {
            playerUpgrades.OnUpgradeUpdateEvent += UpdateUI;
        }

        private void OnDisable()
        {
            playerUpgrades.OnUpgradeUpdateEvent -= UpdateUI;
        }

        public void UpdateUI()
        {
            ClearChilden();
            DisplayUpgradeCell();
        }

        private void DisplayUpgradeCell()
        {
            List<UpgradeBase> upgrades = playerUpgrades.GetUpgrades();
            foreach (UpgradeBase upgrade in upgrades)
            {
                UpgradeCellDisplayer cell = Instantiate(cellPrefab, transform);
                cell.Setup(upgrade.icon);
            }
        }

        private void ClearChilden()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}