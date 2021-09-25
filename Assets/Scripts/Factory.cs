using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Factory : ScriptableObject
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] WaypointsSO Waypoints;
    public void CreateEnemy(Vector3 position)
    {

    }
}
