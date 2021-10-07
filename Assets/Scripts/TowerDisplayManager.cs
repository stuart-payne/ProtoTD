using System;
using System.Collections.Generic;
using System.Linq;
using ProtoTD.Interfaces;
using UnityEngine;

namespace ProtoTD
{
    public class TowerDisplayManager : MonoBehaviour
    {
        [SerializeField] TowerDisplayUI m_TowerDisplayUI;
        private BaseTower m_SelectedTower;
        Func<int, bool> m_FundChecker;
        Action<int> m_FundRemover;

        readonly Dictionary<Strategy, string> m_StrategyStrings = new Dictionary<Strategy, string>
        {
            { Strategy.ClosestToGoal, "FIRST"},
            { Strategy.FurthestFromGoal, "LAST" },
            { Strategy.Strongest, "STRONG" },
            { Strategy.NotSlowed, "SLOW" },
            { Strategy.NotSlowedAndStrongest, "SLOW+STR" }
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

        private void OnDestroy()
        {
            BaseTower.OnTowerClicked -= TowerDisplayListener;
            Placeable.OnPlaceableSpawned -= PlaceableDisplayListener;
            Placeable.OnPlaceableDestroyed -= OnPlaceableDestroyedListener;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                m_TowerDisplayUI.DisableDisplay();
                m_SelectedTower?.DeactivateRangeIndicator();
            }
        }

        void TowerDisplayListener(BaseTower tower)
        {
            // Debug.Log($"StatSO: {tower.Stats.Name} id: {tower.Id.ToString()}");
            m_SelectedTower?.DeactivateRangeIndicator();
            tower.ActivateRangeIndicator();
            m_TowerDisplayUI.gameObject.SetActive(true);
            m_SelectedTower = tower;
            TowerStatsSO stats = tower.GetTowerStats;
            m_TowerDisplayUI.TowerName.SetValue(stats.Name);
            m_TowerDisplayUI.Cost.SetValue($"$ {stats.Cost.ToString()}");
            m_TowerDisplayUI.Strategy.gameObject.SetActive(true);
            string selectedStrategyStr = m_StrategyStrings[tower.GetTargetSelector.SelectedStrategy];
            var stringList = GenerateStrategyList(tower);
            int selectedInd = stringList.IndexOf(selectedStrategyStr);
            m_TowerDisplayUI.Strategy.DropdownBuilder.PopulateInterfaces(stringList, ChangeTowerStrategy, selectedInd);
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
            return m_StrategyStrings.Where(x => targetConfigurable.GetPossibleStrategies.Contains(x.Key)).Select(x => x.Value).ToList();
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
}
