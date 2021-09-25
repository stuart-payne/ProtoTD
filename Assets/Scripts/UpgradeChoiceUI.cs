using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeChoiceUI : MonoBehaviour
{
    [SerializeField] Color m_Available = Color.white;
    [SerializeField] Color m_Unavailable = Color.gray;
    [SerializeField] RowUI m_NameDisplay;
    [SerializeField] RowUI m_CostDisplay;
    [SerializeField] RowUI m_DamageDisplay;
    [SerializeField] RowUI m_FireRangeDisplay;
    [SerializeField] RowUI m_FireCooldownDisplay;
    [SerializeField] Button m_UpgradeButton;
    Image m_ButtonImage;
    Func<int, bool> m_FundChecker;
    int m_CurrentCost;
    public void SetChoiceUI(TowerStatsSO towerStats, UnityAction upgradeCallback, Func<int, bool> fundChecker, UnityAction disableCallback, Action<int> fundRemover)
    {
        m_CurrentCost = towerStats.Cost;
        m_FundChecker = fundChecker;
        m_NameDisplay.SetValue(towerStats.Name);
        m_CostDisplay.SetValue(towerStats.Cost.ToString());
        m_DamageDisplay.SetValue(towerStats.Damage.ToString());
        m_FireCooldownDisplay.SetValue(towerStats.FiringCooldown.ToString());
        m_FireRangeDisplay.SetValue(towerStats.FiringRange.ToString());
        m_UpgradeButton.onClick.RemoveAllListeners();
        m_UpgradeButton.onClick.AddListener(upgradeCallback);
        m_UpgradeButton.onClick.AddListener(disableCallback);
        m_UpgradeButton.onClick.AddListener(() => fundRemover(m_CurrentCost));
    }

    private void Start()
    {
        m_ButtonImage = m_UpgradeButton.GetComponent<Image>();
    }

    private void Update()
    {
        if(m_FundChecker(m_CurrentCost))
        {
            m_UpgradeButton.interactable = true;
            m_ButtonImage.color = m_Available;
        } else
        {
            m_UpgradeButton.interactable = false;
            m_ButtonImage.color = m_Unavailable;
        }
    }
}
