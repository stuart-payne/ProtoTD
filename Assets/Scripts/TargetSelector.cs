using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtoTD
{
    public class TargetSelector
    {
        public GameObject CurrentTarget;
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
            CurrentTarget = m_CurrentStrategy().gameObject;
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
            switch (strategy)
            {
                case Strategy.ClosestToGoal:
                    m_CurrentStrategy = ClosestTargetToGoal;
                    break;
                case Strategy.FurthestFromGoal:
                    m_CurrentStrategy = FurthestTargetFromGoal;
                    break;
                case Strategy.Strongest:
                    m_CurrentStrategy = StrongestEnemy;
                    break;
                case Strategy.NotSlowed:
                    m_CurrentStrategy = NotSlowedAndClosestToEnd;
                    break;
                case Strategy.NotSlowedAndStrongest:
                    m_CurrentStrategy = NotSlowedAndStrongest;
                    break;
            }

            SelectedStrategy = strategy;
        }

        Enemy ClosestTargetToGoal() => m_Targets.OrderBy(x => x.GetDistanceFromEnd()).First();

        Enemy FurthestTargetFromGoal() => m_Targets.OrderByDescending(x => x.GetDistanceFromEnd()).First();

        Enemy StrongestEnemy() => m_Targets.OrderByDescending(x => x.Stats.GetBaseValue(EnemyStat.Health)).First();

        Enemy NotSlowedAndClosestToEnd()
        {
            var targets = m_Targets.OrderBy(x => x.GetDistanceFromEnd());
            return targets.FirstOrDefault(x => !x.Stats.StatusEffects.Has(EnemyStat.Speed)) ?? targets.First();
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