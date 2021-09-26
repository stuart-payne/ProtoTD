using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoTD
{
    public class TowerDisplayUI : MonoBehaviour
    {
        public RowUI TowerName;
        public RowUI Cost;
        public RowUIDropdown Strategy;
        public Button Upgrade;
        public UpgradeUI UpgradeUI;

        private void ResetButtonListeners()
        {
            Upgrade.onClick.RemoveAllListeners();
        }

        public void DisableUpgradeButton()
        {
            Upgrade.gameObject.SetActive(false);
        }

        public void DisableDisplay()
        {
            UpgradeUI.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void AddUpgradeButtonListener(Upgrader upgrader, Func<int, bool> fundChecker, Action<int> fundRemover)
        {
            Upgrade.gameObject.SetActive(true);
            ResetButtonListeners();
            Upgrade.onClick.AddListener(() =>
            {
                UpgradeUI.gameObject.SetActive(true);
                UpgradeUI.SetUpgrader(upgrader, fundChecker, DisableDisplay, fundRemover);
            });
        }
    }
}
