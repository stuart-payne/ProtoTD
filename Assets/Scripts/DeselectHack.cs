using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectHack : MonoBehaviour
{
    [SerializeField] TowerDisplayUI m_TowerDisplay;

    private void OnMouseDown()
    {
        m_TowerDisplay.DisableDisplay();
    }
}
