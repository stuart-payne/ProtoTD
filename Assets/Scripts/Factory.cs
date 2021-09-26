using UnityEngine;

namespace ProtoTD
{
    [CreateAssetMenu()]
    public class Factory : ScriptableObject
    {
        [SerializeField] GameObject EnemyPrefab;
        [SerializeField] WaypointsSO Waypoints;
        public void CreateEnemy(Vector3 position)
        {

        }
    }
}
