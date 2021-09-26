using System.Collections.Generic;
using UnityEngine;

namespace ProtoTD
{
    [CreateAssetMenu()]
    public class UpgradePath :ScriptableObject
    {
        public List<TowerStatsSO> UpgradeList;
    }
}

