using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    public static WaypointManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetNextWaypoint(int index)
    {
        return waypoints[index].transform.position;
    }

    public Vector3 GetStartPoint()
    {
        return waypoints[0].transform.position;
    }
}
