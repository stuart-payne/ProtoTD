using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ProtoTD
{
    [CustomEditor(typeof(ThemeSO))]
    public class ThemeSOInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ThemeSO themeSO = (ThemeSO) target;
            if (GUILayout.Button("Apply Theme"))
            {
                themeSO.ApplyThemeToMaterials();
            }
        }
    }
}
