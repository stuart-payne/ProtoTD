using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProtoTD
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_SettingsPanel;
        private float m_VolumeValue;
        // Start is called before the first frame update
        public void StartGame()
        {
            PlayerPrefs.SetFloat("Volume", m_VolumeValue);
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        }

        public void OpenSettings() => m_SettingsPanel.SetActive(!m_SettingsPanel.activeSelf);
        public void QuitGame()
        {
            PlayerPrefs.SetFloat("Volume", m_VolumeValue);
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #endif
            #if !UNITY_WEBGL
            Application.Quit();
            #endif
        }

        public void OnSliderValueChanged(float value)
        {
            m_VolumeValue = value;
        }
    }
}
