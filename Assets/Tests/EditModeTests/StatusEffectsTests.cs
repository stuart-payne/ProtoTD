using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Tests;

public class StatusEffectsTests
{
    [Test]
    public void AddAndCheckStatusEffectExpectTrue()
    {
        var statusEffects = TestHelpers.SetUpTestStatusEffectsObject();
        statusEffects.Add(TestStats.Damage, 1.2f, 10.0f);
        Assert.AreEqual(statusEffects.Has(TestStats.Damage), true);
    }

    [Test]
    public void HasReturnsFalseWithNullStatusEffect()
    {
        var statusEffects = TestHelpers.SetUpTestStatusEffectsObject();
        Assert.AreEqual(statusEffects.Has(TestStats.Damage), false);
    }

    [Test]
    public void IndexerReturnsOneWhenNoStatusEffect()
    {
        var statusEffects = TestHelpers.SetUpTestStatusEffectsObject();
        Assert.AreEqual(statusEffects[TestStats.Health], 1.0f);
    }

    [Test]
    public void IndexerReturnsModifierWhenAddUsed()
    {
        var statusEffects = TestHelpers.SetUpTestStatusEffectsObject();
        float modifier = 1.2f;
        statusEffects.Add(TestStats.Health, modifier, 10.0f);
        Assert.AreEqual(statusEffects[TestStats.Health], modifier);
    }
}
