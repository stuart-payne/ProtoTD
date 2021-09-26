using UnityEngine;

namespace ProtoTD
{
    public class DeselectHack : MonoBehaviour
    {
        [SerializeField] TowerDisplayUI m_TowerDisplay;

        private void OnMouseDown()
        {
            m_TowerDisplay.DisableDisplay();
        }
    }
}
