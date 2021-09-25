using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;
public class TargetSelecterTests
{
    [Test]
    public void ReturnsTrueAndChangesCurrentTargetWhenEnemyAdded()
    {
        var targetSelecter = new TargetSelector(Strategy.ClosestToGoal);
        var enemyComp = new GameObject().AddComponent<Enemy>();
        enemyComp.Waypoints = GetTestWayPoints();
        targetSelecter.AddTarget(enemyComp);
        Assert.AreEqual(targetSelecter.SelectTarget(), true);
        Assert.AreEqual(targetSelecter.CurrentTarget, enemyComp.gameObject);
    }

    [Test]
    public void ReturnsFalseWhenNoTarget()
    {
        var targetSelecter = new TargetSelector(Strategy.ClosestToGoal);
        Assert.AreEqual(targetSelecter.SelectTarget(), false);
    }

    [Test]
    public void ClosestToWaypointStrategyTest()
    {
        var testEnemies = GenerateTestEnemies();
        var targetSelecter = new TargetSelector(Strategy.ClosestToGoal);
        foreach(var enemy in testEnemies)
        {
            targetSelecter.AddTarget(enemy);
        }
        Assert.AreEqual(targetSelecter.SelectTarget(), true);
        Assert.AreEqual(targetSelecter.CurrentTarget, testEnemies[2].gameObject);
    }

    [Test]
    public void FurthestFromWaypointStrategyTest()
    {
        var testEnemies = GenerateTestEnemies();
        var targetSelecter = new TargetSelector(Strategy.FurthestFromGoal);
        foreach(var enemy in testEnemies)
        {
            targetSelecter.AddTarget(enemy);
        }
        Assert.AreEqual(targetSelecter.SelectTarget(), true);
        Assert.AreEqual(targetSelecter.CurrentTarget, testEnemies[0].gameObject);
    }

    [UnityTest]
    public IEnumerator StrongestEnemyStrategyTest()
    {
        var testEnemies = GenerateTestEnemies();
        var targetSelecter = new TargetSelector(Strategy.Strongest);
        yield return null;
        for(var i = 0; i < testEnemies.Count; i++)
        {
            Enemy enemy = testEnemies[i];
            enemy.Stats[EnemyStat.Health] += i * 10;
            targetSelecter.AddTarget(enemy);
        }
        targetSelecter.ChangeStrategy(Strategy.Strongest);
        Assert.AreEqual(targetSelecter.SelectTarget(), true);
        Assert.AreEqual(targetSelecter.CurrentTarget, testEnemies[testEnemies.Count - 1].gameObject);
    }

    List<Enemy> GenerateTestEnemies()
    {
        var testEnemies = new List<Enemy>();
        var startingPosition = new Vector3(0, 0, 0);
        for (var i = 0; i < 3; i++)
        {
            //var gObj = new GameObject();
            //gObj.transform.position = startingPosition;
            //startingPosition.y += 0.1f;
            //var enemyComp = gObj.AddComponent<Enemy>();
            //enemyComp.StartingStats = GenerateEnemyStatsSO();
            //enemyComp.Waypoints = GetTestWayPoints();
            var enemyObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");
            var instantiatedObj = Object.Instantiate(enemyObj);
            instantiatedObj.transform.position = startingPosition;
            startingPosition.y += 0.1f;
            var enemyComp = instantiatedObj.GetComponent<Enemy>();
            enemyComp.Waypoints = GetTestWayPoints();
            testEnemies.Add(instantiatedObj.GetComponent<Enemy>());
        }
        return testEnemies;
    }

    EnemyStatsSO GenerateEnemyStatsSO()
    {
        var scriptObj = ScriptableObject.CreateInstance<EnemyStatsSO>();
        scriptObj.BaseStats = new List<EnemyStatField>();
        scriptObj.BaseStats.Add(new EnemyStatField(EnemyStat.Health, 10));
        return scriptObj;
    }

    Vector3[] GetTestWayPoints()
    {
        return new [] {
            new Vector3(0, 1, 0),
            new Vector3(0, 2, 0),
            new Vector3(0, 3, 0),
            };
    }
}
