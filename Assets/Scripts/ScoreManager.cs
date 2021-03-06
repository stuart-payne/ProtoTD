using TMPro;
using UnityEngine;

namespace ProtoTD
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int m_Lives;
        [SerializeField] private GameoverPanel m_GameoverPanel;
        [SerializeField] private LevelManager m_LevelManager;
        public TextMeshProUGUI ScoreTextElement;
        public TextMeshProUGUI LivesTextElement;

        public int Lives
        {
            get => m_Lives;
            set
            {
                m_Lives = value;
                UpdateLivesText();
                if (m_Lives == 0) GameOver();
            }
        }

        public int Score
        {
            get { return m_Score; }
            private set
            {
                m_Score = value;
                if (m_Score < 0)
                    m_Score = 0;
                UpdateScoreText();
            }
        }

        private int m_Score = 0;

        void Start()
        {
            UpdateLivesText();
            Enemy.OnDeathEvent += OnEnemyDeath;
            Enemy.OnReachEndEvent += OnEndGoal;
        }

        private void OnDestroy()
        {
            Enemy.OnDeathEvent -= OnEnemyDeath;
            Enemy.OnReachEndEvent -= OnEndGoal;
        }

        void OnEnemyDeath(StatContainer<EnemyStat> stats)
        {
            Score += stats[EnemyStat.ScoreValue];
        }

        void OnEndGoal(StatContainer<EnemyStat> stats)
        {
            Score -= stats[EnemyStat.ScoreValue];
            Lives -= 1;
        }

        void UpdateScoreText()
        {
            ScoreTextElement.text = Score.ToString();
        }

        void UpdateLivesText()
        {
            LivesTextElement.text = Lives.ToString();
        }

        void GameOver()
        {
            Time.timeScale = 0.0f;
            m_GameoverPanel.transform.parent.gameObject.SetActive(true);
            m_GameoverPanel.GiveData(Score, m_LevelManager.RestartScene);
        }
    }
}