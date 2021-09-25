using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected Vector3[] m_Waypoints;
    protected int m_WaypointIndex;
    public void SetWaypoints(Vector3[] waypoints)
    {
        m_Waypoints = waypoints;
    }

    public float GetDistanceFromEnd()
    {
        float distance = 0.0f;
        for (var i = m_WaypointIndex; i < m_Waypoints.Length; i++)
        {
            distance += Vector3.Distance(transform.position, m_Waypoints[i]);
        }
        return distance;
    }
}
