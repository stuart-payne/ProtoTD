using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StatusEffectsTests
{
    // A Test behaves as an ordinary metho

    [UnityTest]
    public IEnumerator SuccessfullyRemoveStatusEffectAfterDuration()
    {
        var statusEffects = SetUpTestStatusEffectsObject();
        float duration = 10.0f;
        float modifier = 2.0f;
        // speed up time
        Time.timeScale = 100.0f;

        statusEffects.Add(TestStats.Health, modifier, duration);
        // verifiy modifier is 2.0f
        Assert.AreEqual(statusEffects.Has(TestStats.Health), true);
        //Assert.AreEqual(statusEffects[TestStats.Health], modifier);
        yield return new WaitForSeconds(duration + 1.0f);
        Assert.AreEqual(statusEffects.Has(TestStats.Health), false);
        Time.timeScale = 1.0f;
    }


    [Test]
    public void AddAndCheckStatusEffectExpectTrue()
    {
        var statusEffects = SetUpTestStatusEffectsObject();
        statusEffects.Add(TestStats.Damage, 1.2f, 10.0f);
        Assert.AreEqual(statusEffects.Has(TestStats.Damage), true);
    }

    [Test]
    public void HasReturnsFalseWithNullStatusEffect()
    {
        var statusEffects = SetUpTestStatusEffectsObject();
        Assert.AreEqual(statusEffects.Has(TestStats.Damage), false);
    }

    [Test]
    public void IndexerReturnsOneWhenNoStatusEffect()
    {
        var statusEffects = SetUpTestStatusEffectsObject();
        Assert.AreEqual(statusEffects[TestStats.Health], 1.0f);
    }

    [Test]
    public void IndexerReturnsModifierWhenAddUsed()
    {
        var statusEffects = SetUpTestStatusEffectsObject();
        float modifier = 1.2f;
        statusEffects.Add(TestStats.Health, modifier, 10.0f);
        Assert.AreEqual(statusEffects[TestStats.Health], modifier);
    }

    class CoroutineProvider : MonoBehaviour, ICoroutineHandler
    { }

    enum TestStats
    {
        Health,
        Damage,
        Speed
    }

    StatusEffects<TestStats> SetUpTestStatusEffectsObject()
    {
        var gObj = new GameObject();
        var coroutineProvider = gObj.AddComponent<CoroutineProvider>();
        return new StatusEffects<TestStats>(coroutineProvider);
    }
}
