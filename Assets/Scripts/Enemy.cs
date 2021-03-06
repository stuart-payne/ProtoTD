using System;
using System.Collections.Generic;
using ProtoTD.Interfaces;
using UnityEngine;

namespace ProtoTD
{
    public class Enemy : MonoBehaviour, ICoroutineHandler
    {
        public float CurrentHealth;
        public EnemyStatsSO StartingStats;
        public Vector3[] Waypoints;
        public int HealthBonus;

        public StatContainer<EnemyStat> Stats;
        [SerializeField] private GameObject m_DeathEffect;

        private int m_WaypointIndex;
        private Vector3 m_CurrentWaypoint;
        private readonly HashSet<ITargeter> m_TowersTargetedBy = new HashSet<ITargeter>();

        public float Speed => Stats[EnemyStat.Speed];


        public float GetDistanceFromEnd()
        {
            var distance = 0.0f;
            for (var i = m_WaypointIndex; i < Waypoints.Length; i++)
                distance += Vector3.Distance(transform.position, Waypoints[i]);

            return distance;
        }

        public void DealDamage(int damage)
        {
            Stats[EnemyStat.Health] -= damage;
        }

        // Start is called before the first frame update
        private void Start()
        {
            Stats = new StatContainer<EnemyStat>(this, StartingStats.GenerateStatDict());
            Stats[EnemyStat.Health] += HealthBonus;
            m_CurrentWaypoint = Waypoints[0];
            Stats.AddListenerToStat(EnemyStat.Health, this, DeathCheck, Die);
        }

        private bool DeathCheck(int health)
        {
            return health <= 0;
        }

        // Update is called once per frame
        private void Update()
        {
            var dir = (m_CurrentWaypoint - transform.position).normalized;
            transform.Translate(dir * Time.deltaTime * Speed);
        }


        private void Die()
        {
            OnDeathEvent?.Invoke(Stats);
            Instantiate(m_DeathEffect, transform.position, m_DeathEffect.transform.rotation);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Waypoint"))
            {
                m_WaypointIndex++;
                m_CurrentWaypoint = Waypoints[m_WaypointIndex];
            }
            else if (other.CompareTag("End"))
            {
                OnReachEndEvent?.Invoke(Stats);
                Destroy(gameObject);
            }
            else if (other.CompareTag("TowerRange"))
            {
                var tower = other.GetComponentInParent<BaseTower>();
                tower.RegisterTarget(this);
                m_TowersTargetedBy.Add(tower);
            }
            else if (other.CompareTag("Projectile"))
            {
                // This is bad
                var projectile = other.GetComponent<Projectile>();
                if (projectile.StatusEffect != null) Stats.StatusEffects.Add(projectile.StatusEffect);

                DealDamage(projectile.Damage);
                Destroy(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("TowerRange")) return;
            var tower = other.GetComponentInParent<BaseTower>();
            tower.DeregisterTarget(this);
            m_TowersTargetedBy.Remove(tower);
        }

        private void OnDestroy()
        {
            foreach (BaseTower tower in m_TowersTargetedBy) tower.DeregisterTarget(this);
        }

        public static event Action<StatContainer<EnemyStat>> OnDeathEvent;
        public static event Action<StatContainer<EnemyStat>> OnReachEndEvent;
    }
}