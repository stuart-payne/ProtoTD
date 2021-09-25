using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour, IDisplayable
{
    public Camera Cam;
    public GameObject Ground;
    public Func<int, bool> HasFundsAvailable;
    public Action<int> RemoveFunds;
    public TowerStatsSO Stats;

    [SerializeField] private Material m_PlaceableMat;
    [SerializeField] private Material m_UnplaceableMat;
    [SerializeField] private GameObject m_PrefabToPlace;
    private Plane m_Plane;
    private bool m_IsPlaceable = true;
    private List<Collider> m_Colliders = new List<Collider>();
    private Collider m_TowerCollider;
    private Renderer rend;
    private int cost = 500;

    public TowerStatsSO GetTowerStats => Stats;

    void Start()
    {
        m_Plane = new Plane(Ground.transform.up, Ground.transform.position);
        m_TowerCollider = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
        OnPlaceableSpawned?.Invoke(this);
    }

    void FixedUpdate()
    {
        var ray = Cam.ScreenPointToRay(Input.mousePosition);
        // Plane.Raycast will give us a distance from the ray's origin that it intersects with the plane.
        // We then use this distance to get the point along the ray and place the position of objetc there
        float dist;
        if(m_Plane.Raycast(ray, out dist))
        {
            transform.position = ray.GetPoint(dist) + (Vector3.up * (m_TowerCollider.bounds.size.y / 2));
        }
    }

    private void Update()
    {
        rend.material = IsPlacable() ? m_PlaceableMat : m_UnplaceableMat;
        if(Input.GetKeyDown(KeyCode.Mouse0) && IsPlacable())
        {
            RemoveFunds(Stats.Cost);
            Instantiate(m_PrefabToPlace, transform.position, m_PrefabToPlace.transform.rotation);
            OnPlaceableDestroyed?.Invoke();
            Destroy(gameObject);
        } else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Path"))
        {
            m_Colliders.Add(other);
            SetPlaceable();
        } else if (other.gameObject.CompareTag("Tower"))
        {
            m_Colliders.Add(other);
            SetPlaceable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Path"))
        {
            m_Colliders.Remove(other);
            SetPlaceable();
        } else if (other.gameObject.CompareTag("Tower"))
        {
            m_Colliders.Remove(other);
            SetPlaceable();
        }
    }

    bool IsPlacable() => m_IsPlaceable && HasFundsAvailable(cost);

    void SetPlaceable()
    {
        if(m_Colliders.Count == 0)
        {
            m_IsPlaceable = true;
        } else
        {
            m_IsPlaceable = false;
        }
    }

    public static event Action<IDisplayable> OnPlaceableSpawned;
    public static event Action OnPlaceableDestroyed;
}
