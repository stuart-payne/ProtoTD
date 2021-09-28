using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProtoTD
{
    public class TitleManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public void StartGame()
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        }

        public void OpenSettings()
        {}

        public void QuitGame()
        {
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #endif
            #if !UNITY_WEBGL
            Application.Quit();
            #endif
        }
    }
}
