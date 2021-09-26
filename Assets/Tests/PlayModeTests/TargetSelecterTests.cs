using System.Collections;
using NUnit.Framework;
using ProtoTD;
using UnityEngine.TestTools;
using ProtoTD.Tests;
using Tests;
public class TargetSelecterTests
{

    [UnityTest]
    public IEnumerator StrongestEnemyStrategyTest()
    {
        var testEnemies = TestHelpers.GenerateTestEnemies();
        var targetSelecter = new TargetSelector(Strategy.Strongest);
        yield return null;
        foreach(var enemy in testEnemies)
        {
            targetSelecter.AddTarget(enemy);
        }
        Assert.AreEqual(targetSelecter.SelectTarget(), true);
        Assert.AreEqual(targetSelecter.CurrentTarget, testEnemies[testEnemies.Count - 1].gameObject);
    }
    
    [UnityTest]
    public IEnumerator StrongestEnemyStrategyTestTest()
    {
        var testEnemies = TestHelpers.GenerateTestEnemies();
        var targetSelecter = new TargetSelector(Strategy.Strongest);
        yield return null;
        foreach(var enemy in testEnemies)
        {
            targetSelecter.AddTarget(enemy);
        }
        Assert.AreEqual(targetSelecter.SelectTarget(), true);
        Assert.AreEqual(targetSelecter.CurrentTarget, testEnemies[testEnemies.Count - 1].gameObject);
    }
}
