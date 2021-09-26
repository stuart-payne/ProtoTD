using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoTD
{
    public enum StatusEffectType
    {
        Slow, Weaken
    }

    [Serializable]
    public class StatusEffect<T> where T: Enum
    {
        public T Type;
        public float Intensity;
        public float Duration;
    }

    public class StatusEffects<T> where T: Enum
    {
        private Dictionary<T, float> m_Modifiers = new Dictionary<T, float>();
        private Dictionary<T, Coroutine> m_StatusDurations = new Dictionary<T, Coroutine>();
        private ICoroutineHandler m_CoroutineHandler;
        private float m_Value;

        public StatusEffects(ICoroutineHandler coroutineHandler)
        {
            m_CoroutineHandler = coroutineHandler;
            m_Modifiers.Clear();
        }

        public void Add(T stat, float modifier, float duration)
        {
            // We need to know if a modifier already exists and decide which then lasts.
            if(m_Modifiers.ContainsKey(stat))
            {
                // if current modifier is larger than one being applied, we don't want to overwrite it so just return
                if (m_Modifiers[stat] > modifier)
                    return;
                // Stop old coroutine as a new duration will be made and the previous modifier will be overwritten
                m_CoroutineHandler.StopCoroutine(m_StatusDurations[stat]);
            }
            m_Modifiers[stat] = modifier;
            m_StatusDurations[stat] = m_CoroutineHandler.StartCoroutine(DurationCoroutine(duration, stat));
        }

        public void Add(StatusEffect<T> statusEffect) => Add(statusEffect.Type, statusEffect.Intensity, statusEffect.Duration);

        public bool Has(T stat) => m_Modifiers.ContainsKey(stat);

        public float GetModifier(T stat) => m_Modifiers[stat];

        public float this[T stat] => m_Modifiers.TryGetValue(stat, out m_Value) ? m_Value : 1.0f;
    
        public Dictionary<T, float> GetModifierDict() => m_Modifiers;

        IEnumerator DurationCoroutine(float duration, T stat)
        {
            yield return new WaitForSeconds(duration);
            m_Modifiers.Remove(stat);
            m_StatusDurations.Remove(stat);
        }
    }
}