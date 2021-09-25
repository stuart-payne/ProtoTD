using Tests;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;

public class StatsTest
{
    [Test]
    public void IndexerShouldReturnCorrectValueInitialised()
    {
        var stats = SetupStatContainer();
        Assert.AreEqual(stats[TestStats.Health], m_TestValues[TestStats.Health]);
    }

    [Test]
    public void IndexerModifyValueCorrectly()
    {
        var stats = SetupStatContainer();
        int subtractionAmount = 5;
        int expectedValue = m_TestValues[TestStats.Health] - subtractionAmount;
        Assert.AreEqual(stats[TestStats.Health] - subtractionAmount, expectedValue);
    }

    [Test]
    public void EventSuccessfullyFiredOnConditionMet()
    {
        var stats = SetupStatContainer();
        bool callbackFired = false;
        Func<int, bool> condition = (int x) => (x <= 0);
        Action callback = () => { callbackFired = true; };
        stats.AddListenerToStat(TestStats.Health, this, condition, callback);
        stats[TestStats.Health] -= 20;
        Assert.AreEqual(callbackFired, true);
    }

    [Test]
    public void EventNotFiredOnConditionNotMet()
    {
        var stats = SetupStatContainer();
        bool callbackFired = false;
        Func<int, bool> condition = (int x) => (x <= 0);
        Action callback = () => { callbackFired = true; };
        stats.AddListenerToStat(TestStats.Health, this, condition, callback);
        stats[TestStats.Health] -= 1;
        Assert.AreEqual(callbackFired, false);
    }

    [Test]
    public void DeregisterEvent()
    {
        var stats = SetupStatContainer();
        bool callbackFired = false;
        Func<int, bool> condition = (int x) => (x <= 0);
        Action callback = () => { callbackFired = true; };
        stats.AddListenerToStat(TestStats.Health, this, condition, callback);
        stats.RemoveListenerToStat(TestStats.Health, this);
        stats[TestStats.Health] -= 20;
        Assert.AreEqual(callbackFired, false);
    }

    StatContainer<TestStats> SetupStatContainer()
    {
        var gObj = new GameObject();
        var enemyComp = gObj.AddComponent<CoroutineProvider>();
        return new StatContainer<TestStats>(enemyComp, m_TestValues);

    }
    
    Dictionary<TestStats, int> m_TestValues = new Dictionary<TestStats, int> 
    { 
        { TestStats.Damage, 1 }, 
        { TestStats.Health, 10 }, 
        { TestStats.Speed, 5 } 
    };
}
