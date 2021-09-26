using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtoTD
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject m_StartPoint;
        [SerializeField] GameObject[] m_Waypoints;
        [SerializeField] GameObject m_enemyPrefab;
        [SerializeField] GameObject[] m_PlaceablePrefabs;
        [SerializeField] GameObject m_GroundObject;
        [SerializeField] Camera m_Camera;
        [SerializeField] BuildTowerDropdown m_BuildTowerDropdown;
        [SerializeField] float m_PerWaveHealthIncrease = 0.5f;
        private GameObject m_CurrentPlaceable;
        private Money m_Money;
        // Start is called before the first frame update
        void Start()
        {
            m_Money = GetComponent<Money>();
            m_BuildTowerDropdown.PopulateInterfaces(GetDropdownOptions(), index => SpawnPlaceableTower(index), 0);
            //InvokeRepeating("SpawnEnemy", 2.0f, 2.0f);
        }

        public List<string> GetDropdownOptions()
        {
            return m_PlaceablePrefabs.Select(x => {
                var placeable = x.GetComponent<Placeable>();
                return $"{placeable.Stats.Name} ${placeable.Stats.Cost}";
            }).ToList();
        }

        public void SpawnEnemy(GameObject enemyPrefab, int waveNumber)
        {
            GameObject enemy = Instantiate(enemyPrefab, m_StartPoint.transform.position, enemyPrefab.transform.rotation);
            var enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.Waypoints = GetWaypointArray();
            var baseHealth = enemyComponent.StartingStats.BaseStats.First(x => x.Stat == EnemyStat.Health).Value;
            enemyComponent.HealthBonus = (int)(baseHealth * m_PerWaveHealthIncrease * waveNumber);
        }

        public void SpawnPlaceableTower(int placeableIndex)
        {
            if (m_CurrentPlaceable == null)
            {
                m_CurrentPlaceable = Instantiate(m_PlaceablePrefabs[placeableIndex]);
                var placeable = m_CurrentPlaceable.GetComponent<Placeable>();
                placeable.Ground = m_GroundObject;
                placeable.Cam = m_Camera;
                placeable.HasFundsAvailable = m_Money.HasFundsAvailable;
                placeable.RemoveFunds = m_Money.RemoveFunds;
            }
        }

        Vector3[] GetWaypointArray()
        {
            return m_Waypoints.Select(x => x.transform.position).ToArray();
        }
    }
}
