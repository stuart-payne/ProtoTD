using TMPro;
using UnityEngine;

namespace ProtoTD
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_MoneyText;

        public int MoneyAvailable
        {
            get => _moneyAvailable;
            private set
            {
                _moneyAvailable = value;
                UpdateText();
            }
        }

        private int _moneyAvailable = 500;

        private void Start()
        {
            Enemy.OnDeathEvent += AddFundsOnEnemyDeath;
            UpdateText();
        }

        void UpdateText() => m_MoneyText.text = MoneyAvailable.ToString();
        public bool HasFundsAvailable(int money) => MoneyAvailable >= money;

        public void RemoveFunds(int money) => MoneyAvailable -= money;
        public void AddFunds(int money) => MoneyAvailable += money;

        public void AddFundsOnEnemyDeath(StatContainer<EnemyStat> stats)
        {
            MoneyAvailable += stats[EnemyStat.MoneyValue];
        }
    }
}