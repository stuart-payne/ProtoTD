using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves/WaveSO")]
public class WaveSO : ScriptableObject
{
    public EnemySpawnString[] SpawnStrings;
}

[Serializable]
public struct EnemySpawnString
{
    public float StartDelay;
    public float TimeBetweenSpawns;
    public GameObject EnemyPrefab;
    public int numberOfEnemies;
}
