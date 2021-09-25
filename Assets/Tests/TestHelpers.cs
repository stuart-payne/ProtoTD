using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class TestHelpers
    {
        public static List<Enemy> GenerateTestEnemies()
        {
            var testEnemies = new List<Enemy>();
            var startingPosition = new Vector3(0, 0, 0);
            for (var i = 0; i < 3; i++)
            {
                var enemyObj = new GameObject();
                enemyObj.transform.position = startingPosition;
                startingPosition.y += 0.1f;
                var enemyComp = enemyObj.AddComponent<Enemy>();
                enemyComp.Waypoints = GetTestWayPoints();
                var enemyBaseStats = new Dictionary<EnemyStat, int>
                {
                    {EnemyStat.Speed, 1},
                    {EnemyStat.Health, 1 + i}
                };
                enemyComp.StartingStats = GenerateEnemyStatsSO(enemyBaseStats);
                testEnemies.Add(enemyComp);
            }
            return testEnemies;
        }
    
        public static EnemyStatsSO GenerateEnemyStatsSO(Dictionary<EnemyStat, int> baseStats)
        {
            var scriptObj = ScriptableObject.CreateInstance<EnemyStatsSO>();
            scriptObj.BaseStats = new List<EnemyStatField>();
            foreach (var baseStat in baseStats)
            {
                scriptObj.BaseStats.Add(new EnemyStatField(baseStat.Key, baseStat.Value));
            }
            return scriptObj;
        }
    
        public static Vector3[] GetTestWayPoints()
        {
            return new [] {
                new Vector3(0, 1, 0),
                new Vector3(0, 2, 0),
                new Vector3(0, 3, 0),
            };
        }
    }
}