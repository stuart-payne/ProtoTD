using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtoTD
{
    [CreateAssetMenu()]
    public class EnemyStatsSO : ScriptableObject
    {
        public string Name;
        public List<EnemyStatField> BaseStats;

        public Dictionary<EnemyStat, int> GenerateStatDict()
        {
            var dict = new Dictionary<EnemyStat, int>();
            foreach(var stat in BaseStats)
            {
                dict.Add(stat.Stat, stat.Value);
            }
            return dict;
        }

        public int GetStatValue(EnemyStat stat)
        {
            return BaseStats.First(x => x.Stat == stat).Value;
        }
    }

    [Serializable]
    public class EnemyStatField
    {
        public EnemyStat Stat;
        public int Value;

        public EnemyStatField(EnemyStat stat, int value)
        {
            Stat = stat;
            Value = value;
        }
    }

    public enum EnemyStat
    {
        Health,
        ScoreValue,
        MoneyValue,
        Speed,
        DamageTaken
    }
}