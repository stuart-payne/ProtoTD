using System;
using UnityEngine;

namespace ProtoTD
{
    [CreateAssetMenu(menuName = "Themes/MaterialBinding")]
    public class MaterialBindingSO : ScriptableObject
    {
        public TowerMaterialBinding[] TowerMats;
        public Material PathMat;
        public Material GroundMat;
        public EnemyMaterialBinding[] EnemyMat;

        [Serializable]
        public class TowerMaterialBinding
        {
            public TowerTypes TowerType;
            public Material TowerMat;

            public TowerMaterialBinding(TowerTypes towerType, Material towerMat)
            {
                TowerMat = towerMat;
                TowerType = towerType;
            }
        }

        [Serializable]
        public class EnemyMaterialBinding
        {
            public EnemyTypes EnemyType;
            public Material EnemyMat;
        }
    }
}
