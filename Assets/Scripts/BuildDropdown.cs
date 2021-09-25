using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BuildDropdown : MonoBehaviour
{
    [SerializeField] protected TMP_Dropdown m_Dropdown;
    // Start is called before the first frame update
    protected virtual void Start()
    {
;
    }

    public virtual void PopulateInterfaces(List<string> options, UnityAction<int> listener)
    {
        m_Dropdown.ClearOptions();
        m_Dropdown.AddOptions(options);
        m_Dropdown.onValueChanged.AddListener(listener);
    }
}
