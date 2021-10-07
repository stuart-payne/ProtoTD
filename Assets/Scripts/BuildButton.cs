using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoTD
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private GameObject m_BuildMenu;

        public void BuildMenuSwitch() => m_BuildMenu.SetActive(!m_BuildMenu.activeSelf);

    }
}
