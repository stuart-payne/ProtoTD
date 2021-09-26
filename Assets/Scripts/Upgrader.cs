namespace ProtoTD
{
    public class Upgrader
    {
        private UpgradePath[] m_UpgradePaths;
        private int m_UpgradePathChoice;
        private BaseTower m_Tower;
        public int Level = 1;

        public Upgrader(UpgradePath[] upgradePaths, BaseTower tower)
        {
            m_UpgradePaths = upgradePaths;
            m_Tower = tower;
        }

        public TowerStatsSO[] GetUpgradeOptions()
        {
            if(Level == 1)
            {
                return new TowerStatsSO[] { m_UpgradePaths[0].UpgradeList[0], m_UpgradePaths[1].UpgradeList[0] };
            } else
            {
                return new TowerStatsSO[] { m_UpgradePaths[m_UpgradePathChoice].UpgradeList[Level - 1] };
            }
        }

        public bool Upgradeable()
        {
            if (Level == 1)
                return true;

            if (Level > m_UpgradePaths[m_UpgradePathChoice].UpgradeList.Count)
                return false;
            else
                return true;
        }

        public void Upgrade(int choice)
        {
            if(Level == 1)
            {
                m_UpgradePathChoice = choice;
            }
            m_Tower.Stats = m_UpgradePaths[choice].UpgradeList[Level - 1];
            m_Tower.UpdateFiringRange();
            m_Tower.UpdateDefaultStrategy();
            Level++;

        }
    }
}
