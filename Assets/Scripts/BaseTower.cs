using System;
using System.Collections;
using ProtoTD.Interfaces;
using UnityEngine;

namespace ProtoTD
{
    public abstract class BaseTower : MonoBehaviour, ITargeter, IDisplayable, ITargetConfigurable
    {
        public TowerStatsSO Stats;
        public int Id;

        [SerializeField] private string m_TowerName;
        [SerializeField] private Strategy[] m_Strategies;
        [SerializeField] private UpgradePath[] m_UpgradePaths;
        [SerializeField] private SphereCollider TargetRangeCollider;
        [SerializeField] private GameObject m_RangeIndicator;
        private RangeIndicator m_RangeIndicatorComp;
        protected Upgrader m_Upgrader;
        protected TargetSelector m_TargetSelector;
        protected bool m_ReadyToShoot = true;

        public TowerStatsSO GetTowerStats => Stats;
        public TargetSelector GetTargetSelector => m_TargetSelector;

        public Upgrader GetUpgrader => m_Upgrader;

        public Strategy[] GetPossibleStrategies => Stats.PossibleStrategies;

        public void ActivateRangeIndicator()
        {
            m_RangeIndicator.SetActive(true);
            if (!m_RangeIndicatorComp)
                m_RangeIndicatorComp = m_RangeIndicator.GetComponent<RangeIndicator>();
            m_RangeIndicatorComp.UpdateRadius(Stats.FiringRange);
        }

        public void DeactivateRangeIndicator() => m_RangeIndicator.SetActive(false);
        
        public void RegisterTarget(Enemy target)
        {
            m_TargetSelector.AddTarget(target);
        }

        public void DeregisterTarget(Enemy target)
        {
            m_TargetSelector.RemoveTarget(target);
        }

        protected virtual void Start()
        {
            m_TargetSelector = new TargetSelector(Stats.DefaultStrategy);
            Id = TowerCounter++;
            m_Upgrader = new Upgrader(m_UpgradePaths, this);
            UpdateFiringRange();
        }

        public void UpdateFiringRange()
        {
            TargetRangeCollider.radius = Stats.FiringRange;
            if (!m_RangeIndicatorComp)
                m_RangeIndicatorComp = m_RangeIndicator.GetComponent<RangeIndicator>();
            m_RangeIndicatorComp.UpdateRadius(Stats.FiringRange);
        }
        
        public void UpdateDefaultStrategy()
        {
            m_TargetSelector.ChangeStrategy(Stats.DefaultStrategy);
        }

        protected virtual void Update()
        {
            if (m_ReadyToShoot)
            {
                if (m_TargetSelector.SelectTarget())
                {
                    Fire();
                    m_ReadyToShoot = false;
                    StartCoroutine(ShootCooldown());

                }
            }
        }

        protected abstract void Fire();

        protected IEnumerator ShootCooldown()
        {
            yield return new WaitForSeconds(Stats.FiringCooldown);
            m_ReadyToShoot = true;
        }

        private void OnMouseDown()
        {
            OnTowerClicked?.Invoke(this);
        }

        public static event Action<BaseTower> OnTowerClicked;
        private static int TowerCounter = 0;
    }
}
