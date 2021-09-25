using System;
using System.Collections.Generic;
public class StatContainer<T> where T: Enum
{
    public readonly StatusEffects<T> StatusEffects;
    private Dictionary<T, StatValue> m_Values = new Dictionary<T, StatValue>();

    public StatContainer(ICoroutineHandler coroutineHandler, Dictionary<T, int> baseValues)
    {
        StatusEffects = new StatusEffects<T>(coroutineHandler);
        foreach(var baseValue in baseValues)
        {
            m_Values.Add(baseValue.Key, new StatValue(baseValue.Value));
        }
    }

    public int this[T stat]
    {
        get => (int)(m_Values[stat].CurrentValue * StatusEffects[stat]);
        set => m_Values[stat].CurrentValue = value;
    }

    public Dictionary<string, int> GetStatsWithNames()
    {
        var dict = new Dictionary<string, int>();
        foreach(var entry in m_Values)
        {
            dict.Add(Enum.GetName(typeof(T), entry.Key), entry.Value.CurrentValue);
        }
        return dict;
    }


    public int GetBaseValue(T stat) => m_Values[stat].BaseValue;

    public void AddListenerToStat(T stat, object listener, Func<int, bool> condition, Action callback)
    {
        m_Values[stat].AddListener(listener, condition, callback);
    }

    public void RemoveListenerToStat(T stat, object listener)
    {
        m_Values[stat].RemoveListener(listener);
    }
}

public class StatValue
{
    public readonly int BaseValue;
    public int CurrentValue
    {
        get => m_CurrentValue;
        set
        {
            m_CurrentValue = value;
            OnValueChanged();
        }
    }

    private int m_CurrentValue;
    private Dictionary<object, ListenerProperties> m_Listeners = new Dictionary<object, ListenerProperties>();
    private void OnValueChanged()
    {
        foreach(var listener in m_Listeners)
        {
            if (listener.Value.Condition(m_CurrentValue))
                listener.Value.Callback();
        }
    }

    public StatValue(int baseValue)
    {
        BaseValue = baseValue;
        m_CurrentValue = baseValue;
    }

    public void AddListener(object listener, Func<int, bool> condition, Action callback) 
    { 
        m_Listeners.Add(listener, new ListenerProperties(condition, callback)); 
    }

    public void RemoveListener(object listener) => m_Listeners.Remove(listener);

    private class ListenerProperties
    {
        public ListenerProperties(Func<int, bool> condition, Action callback)
        {
            Condition = condition;
            Callback = callback;
        }

        public readonly Func<int, bool> Condition;
        public readonly Action Callback;
    }
}
