using System;
using UnityEngine;
using UnityEngine.Events;

namespace ProtoTD
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] UpgradeChoiceUI m_FirstChoice;
        [SerializeField] UpgradeChoiceUI m_SecondChoice;
        public Upgrader UpgraderObj;

        public void SetUpgrader(Upgrader upgrader, Func<int, bool> fundChecker, UnityAction disableCallback, Action<int> fundRemover)
        {
            UpgraderObj = upgrader;
            TowerStatsSO[] choices = UpgraderObj.GetUpgradeOptions();
            m_FirstChoice.SetChoiceUI(choices[0], () => UpgraderObj.Upgrade(0), fundChecker, disableCallback, fundRemover);
            if (UpgraderObj.Level == 1)
            {
                m_SecondChoice.gameObject.SetActive(true);
                m_SecondChoice.SetChoiceUI(choices[1], () => UpgraderObj.Upgrade(1), fundChecker, disableCallback, fundRemover);
            } else
            {
                m_SecondChoice.gameObject.SetActive(false);
            }
        }
    }
}
