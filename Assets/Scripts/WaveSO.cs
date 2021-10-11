using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace ProtoTD
{
    [CreateAssetMenu(menuName = "Waves/WaveSO")]
    public class WaveSO : ScriptableObject
    {
        public EnemySpawnString[] SpawnStrings;
        
        // define what difficulty is

        public float CalculateDifficulty()
        {
            var stringDifficulties = new List<float>();
            Enemy enemyComp;
            int health;
            int speed;
            foreach (var spawnString in SpawnStrings)
            {
                enemyComp = spawnString.EnemyPrefab.GetComponent<Enemy>(); 
                health = enemyComp.StartingStats.GetStatValue(EnemyStat.Health);
                speed = enemyComp.StartingStats.GetStatValue(EnemyStat.Speed);
                stringDifficulties.Add(UseDifficultyAlgo(
                    health, 
                    spawnString.TimeBetweenSpawns, 
                    speed, 
                    spawnString.numberOfEnemies));
            }

            return stringDifficulties.Sum();
        }

        private float UseDifficultyAlgo(int health, float spawnRate, int speed, int numberOfEnemies)
        {
            return ((health * speed) / spawnRate) * numberOfEnemies;
        }
    }

    [Serializable]
    public struct EnemySpawnString
    {
        public float StartDelay;
        public float TimeBetweenSpawns;
        public GameObject EnemyPrefab;
        public int numberOfEnemies;
    }

    [CustomEditor(typeof(WaveSO))]
    public class WaveSOInspector : Editor
    {
        private string m_CachedDifficulty = "Run Difficulty Button";
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            WaveSO waveSO = (WaveSO) target;
            if (GUILayout.Button("CalculateDifficulty"))
            {
                m_CachedDifficulty = waveSO.CalculateDifficulty().ToString();
            }
            
            GUILayout.Label($"Difficulty of wave: {m_CachedDifficulty}");
        }
    }
}