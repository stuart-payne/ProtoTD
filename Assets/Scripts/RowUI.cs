using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RowUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Label;
    [SerializeField] TextMeshProUGUI m_Value;

    public void SetValue(string value)
    {
        m_Value.text = value;
    }
}
