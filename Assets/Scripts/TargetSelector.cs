using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtoTD
{
    public class TargetSelector
    {
        public Enemy CurrentTarget;
        public Strategy SelectedStrategy;
        private List<Enemy> m_Targets = new List<Enemy>();
        private Func<Enemy> m_CurrentStrategy;

        public TargetSelector(Strategy startingStrategy)
        {
            ChangeStrategy(startingStrategy);
        }

        public bool SelectTarget()
        {
            if (m_Targets.Count == 0)
                return false;
            CurrentTarget = m_CurrentStrategy();
            return true;
        }

        public void AddTarget(Enemy enemy)
        {
            m_Targets.Add(enemy);
        }

        public void RemoveTarget(Enemy enemy)
        {
            m_Targets.Remove(enemy);
        }

        public void ChangeStrategy(Strategy strategy)
        {
            m_CurrentStrategy = strategy switch
            {
                Strategy.ClosestToGoal => ClosestTargetToGoal,
                Strategy.FurthestFromGoal => FurthestTargetFromGoal,
                Strategy.Strongest => StrongestEnemy,
                Strategy.NotSlowed => NotSlowedAndClosestToEnd,
                Strategy.NotSlowedAndStrongest => NotSlowedAndStrongest,
                _ => m_CurrentStrategy
            };

            SelectedStrategy = strategy;
        }

        private Enemy ClosestTargetToGoal()
        {
            return m_Targets
                .OrderBy(x => x.GetDistanceFromEnd())
                .FirstOrDefault(x => x.Stats[EnemyStat.Health] > 0);
        }

        Enemy FurthestTargetFromGoal() => m_Targets.OrderByDescending(x => x.GetDistanceFromEnd()).FirstOrDefault();

        Enemy StrongestEnemy() => m_Targets.OrderByDescending(x => x.Stats.GetBaseValue(EnemyStat.Health)).FirstOrDefault();

        Enemy NotSlowedAndClosestToEnd()
        {
            var targets = m_Targets.OrderBy(x => x.GetDistanceFromEnd());
            return targets.FirstOrDefault(x => !x.Stats.StatusEffects.Has(EnemyStat.Speed)) ?? targets.FirstOrDefault();
        }

        Enemy NotSlowedAndStrongest()
        {
            var targets = m_Targets.OrderBy(x => x.Stats.GetBaseValue(EnemyStat.Health));
            return targets.FirstOrDefault(x => !x.Stats.StatusEffects.Has(EnemyStat.Speed)) ?? targets.First();
        }
    }

    public enum Strategy
    {
        ClosestToGoal,
        FurthestFromGoal,
        Strongest,
        NotSlowed,
        NotSlowedAndStrongest
    }
}