using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProtoTD
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_WelcomeMessage;
        void Start()
        {
            if (PlayerPrefs.HasKey("Volume"))
            {
                AudioListener.volume = PlayerPrefs.GetFloat("Volume");
            }
            
            if(Application.isEditor)
                PlayerPrefs.DeleteKey("HasPlayed");

            if (!PlayerPrefs.HasKey("HasPlayed"))
            {
                m_WelcomeMessage.SetActive(true);
            }
        }

        public void StoreHasPlayed() => PlayerPrefs.SetInt("HasPlayed", 1);
        public void CloseWelcomeMessage() => m_WelcomeMessage.SetActive(false);
        
        public void RestartScene()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
