using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProtoTD
{
    public class LevelManager : MonoBehaviour
    {

        void Start()
        {
            if (PlayerPrefs.HasKey("Volume"))
            {
                AudioListener.volume = PlayerPrefs.GetFloat("Volume");
            }
        }
        public void RestartScene()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
