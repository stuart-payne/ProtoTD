using UnityEngine;

namespace ProtoTD
{
    public class RangeIndicator : MonoBehaviour
    {
        public void UpdateRadius(float radius)
        {
            // radius needs to be doubled for scale to be accurate
            var lossyScale = transform.lossyScale;
            transform.localScale = new Vector3((1 /lossyScale.x), 1 /lossyScale.y, 1/lossyScale.z) * (radius * 2); 
        }
    }
}
