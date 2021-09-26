using UnityEngine;

namespace ProtoTD
{
    public class RangeIndicator : MonoBehaviour
    {
        public void UpdateRadius(float radius)
        {
            // radius needs to be doubled for scale to be accurate
            transform.localScale = new Vector3(1, 1, 1) * (radius * 2); 
        }
    }
}
