using UnityEngine;

namespace ProtoTD
{
    public class RangeIndicator : MonoBehaviour
    {
        private float m_CachedRadius;
        public void UpdateRadius(float radius)
        {
            if (m_CachedRadius != radius)
            {
                var cachedTransform = transform;
                var lossyScale = cachedTransform.parent.localScale;
                cachedTransform.localScale = new Vector3((1 /lossyScale.x), 1 /lossyScale.y, 1/lossyScale.z) * (radius * 2);
                m_CachedRadius = radius;
            }
        }
    }
}
