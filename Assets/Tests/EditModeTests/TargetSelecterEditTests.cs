using NUnit.Framework;
using ProtoTD;
using UnityEngine.TestTools;
using UnityEngine;
using ProtoTD.Tests;
using Tests;

namespace Tests.EditModeTests
{
    public class TargetSelecterEditTests
    {
            
        [Test]
        public void ReturnsTrueAndChangesCurrentTargetWhenEnemyAdded()
        {
            var targetSelecter = new TargetSelector(Strategy.ClosestToGoal);
            var enemyComp = new GameObject().AddComponent<Enemy>();
            enemyComp.Waypoints = TestHelpers.GetTestWayPoints();
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
            var testEnemies = TestHelpers.GenerateTestEnemies();
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
            var testEnemies = TestHelpers.GenerateTestEnemies();
            var targetSelecter = new TargetSelector(Strategy.FurthestFromGoal);
            foreach(var enemy in testEnemies)
            {
                targetSelecter.AddTarget(enemy);
            }
            Assert.AreEqual(targetSelecter.SelectTarget(), true);
            Assert.AreEqual(targetSelecter.CurrentTarget, testEnemies[0].gameObject);
        }
    }
}