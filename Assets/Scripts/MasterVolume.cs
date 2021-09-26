using UnityEngine;
using UnityEngine.UI;

namespace ProtoTD
{
    public class MasterVolume : MonoBehaviour
    {
        public Slider VolumeSlider;

        private void Start()
        {
            VolumeSlider.onValueChanged.AddListener(MasterVolumeUpdate);
        }

        public void MasterVolumeUpdate(float value)
        {
            AudioListener.volume = value;
        }
    }
}
