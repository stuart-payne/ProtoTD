using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerDisplayManager : MonoBehaviour
{
    [SerializeField] TowerDisplayUI m_TowerDisplayUI;
    private BaseTower m_SelectedTower;
    Func<int, bool> m_FundChecker;
    Action<int> m_FundRemover;

    readonly Dictionary<Strategy, string> m_StrategyStrings = new Dictionary<Strategy, string>
    {
        { Strategy.ClosestToGoal, "First"},
        { Strategy.FurthestFromGoal, "Last" },
        { Strategy.Strongest, "Strongest" }
    };
    // Start is called before the first frame update
    void Start()
    {
        BaseTower.OnTowerClicked += TowerDisplayListener;
        Placeable.OnPlaceableSpawned += PlaceableDisplayListener;
        Placeable.OnPlaceableDestroyed += OnPlaceableDestroyedListener;
        var moneyComponent = GetComponent<Money>();
        m_FundChecker = moneyComponent.HasFundsAvailable;
        m_FundRemover = moneyComponent.RemoveFunds;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_TowerDisplayUI.DisableDisplay();
        }
    }

    void TowerDisplayListener(BaseTower tower)
    {
        Debug.Log($"StatSO: {tower.Stats.Name} id: {tower.Id.ToString()}");
        m_TowerDisplayUI.gameObject.SetActive(true);
        m_SelectedTower = tower;
        TowerStatsSO stats = tower.GetTowerStats;
        m_TowerDisplayUI.TowerName.SetValue(stats.Name);
        m_TowerDisplayUI.Cost.SetValue($"$ {stats.Cost.ToString()}");
        m_TowerDisplayUI.Strategy.gameObject.SetActive(true);
        m_TowerDisplayUI.Strategy.DropdownBuilder.PopulateInterfaces(GenerateStrategyList(tower), ChangeTowerStrategy);
        var upgrader = tower.GetUpgrader;
        if (upgrader.Upgradeable())
            m_TowerDisplayUI.AddUpgradeButtonListener(tower.GetUpgrader, m_FundChecker, m_FundRemover);
        else
            m_TowerDisplayUI.DisableUpgradeButton();

    }

    void ChangeTowerStrategy(int index)
    {
        m_SelectedTower.GetTargetSelector.ChangeStrategy(m_StrategyStrings.ElementAt(index).Key);
    }

    List<string> GenerateStrategyList(ITargetConfigurable targetConfigurable)
    {
        return targetConfigurable.GetPossibleStrategies.Select(x => m_StrategyStrings[x]).ToList();
    }

    void PlaceableDisplayListener(IDisplayable placeable)
    {
        m_SelectedTower = null;
        var stats = placeable.GetTowerStats;
        m_TowerDisplayUI.TowerName.SetValue(stats.Name);
        m_TowerDisplayUI.Cost.SetValue($"$ {stats.Cost.ToString()}");
        m_TowerDisplayUI.gameObject.SetActive(true);
        m_TowerDisplayUI.Strategy.gameObject.SetActive(false);
    }

    void OnPlaceableDestroyedListener()
    {
        m_TowerDisplayUI.gameObject.SetActive(false);
    }
}
