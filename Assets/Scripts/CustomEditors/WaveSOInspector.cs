using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ProtoTD;

namespace ProtoTD.CustomEditors
{
    [CustomEditor(typeof(WaveSO))]
    public class WaveSOInspector : Editor
    {
        private string m_CachedDifficulty = "Run Difficulty Button";
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            WaveSO waveSO = (WaveSO) target;
            if (GUILayout.Button("CalculateDifficulty"))
            {
                m_CachedDifficulty = waveSO.CalculateDifficulty().ToString();
            }
            
            GUILayout.Label($"Difficulty of wave: {m_CachedDifficulty}");
        }
    }
}


