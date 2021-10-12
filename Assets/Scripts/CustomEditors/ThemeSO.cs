using System;
using UnityEditor;
using UnityEngine;

namespace ProtoTD.CustomEditors
{
    [CreateAssetMenu(menuName = "Themes/Theme")]
    public class ThemeSO : ScriptableObject
    {
        public Color[] TowerColors;
        public Color PathColor;
        public Color[] GroundColors;
        public Color[] EnemyColors;
        public MaterialBindingSO MaterialBinding;

        public void ApplyThemeToMaterials()
        {
            ApplyThemeToTower();
        }

        private void ApplyThemeToTower()
        {
            Debug.Log("It's a me mario");
            foreach (var towerMat in MaterialBinding.TowerMats)
            {
                foreach (var props in MaterialEditor.GetMaterialProperties(new[] {towerMat.TowerMat}))
                {
                    Debug.Log($"Type is {props.type.ToString()}");
                    Debug.Log($"{props.name}");
                }
                towerMat.TowerMat.SetColor("Primary", TowerColors[0]);
                towerMat.TowerMat.SetColor("Secondary", TowerColors[1]);
            }
        }
    }
}