using UnityEngine;

namespace ProtoTD
{
    public class DestroyEffectOnFinish : MonoBehaviour
    {
        [SerializeField] private AudioSource m_EffectSound;
        [SerializeField] private ParticleSystem m_EffectParticles;

        // Update is called once per frame
        void Update()
        {
            if (!m_EffectSound.isPlaying && !m_EffectParticles.isPlaying)
            {
                Debug.Log($"{m_EffectParticles.isPlaying} & {m_EffectSound.isPlaying}");
                Destroy(gameObject);
            }
        }
    }
}
