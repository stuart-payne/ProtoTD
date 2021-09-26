using TMPro;
using UnityEngine;

namespace ProtoTD
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_Label;
        [SerializeField] TextMeshProUGUI m_Value;

        public void SetValue(string value)
        {
            m_Value.text = value;
        }
    }
}
