using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using ProtoTD.Tests;
using Tests;

namespace Tests.PlayModeTests
{
    public class StatusEffectsTests
    {
        [UnityTest]
        public IEnumerator SuccessfullyRemoveStatusEffectAfterDuration()
        {
            var statusEffects = TestHelpers.SetUpTestStatusEffectsObject();
            float duration = 10.0f;
            float modifier = 2.0f;
            Time.timeScale = 100.0f;

            statusEffects.Add(TestStats.Health, modifier, duration);
            // verifiy modifier is 2.0f
            Assert.AreEqual(statusEffects.Has(TestStats.Health), true);
            yield return new WaitForSeconds(duration + 1.0f);
            Assert.AreEqual(statusEffects.Has(TestStats.Health), false);
            Time.timeScale = 1.0f;
        }
    }
}