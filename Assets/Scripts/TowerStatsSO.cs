using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TowerStatsSO : ScriptableObject
{
    public string Name;
    public int Cost;
    public int Damage;
    public float FiringCooldown;
    public float FiringRange;
    public bool AppliesStatusEffect;
    public StatusEffect<EnemyStat> StatusEffect;
    public Strategy DefaultStrategy;
    public Strategy[] PossibleStrategies;
}