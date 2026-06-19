using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField, Required] AudioSource audioSource;
        [SerializeField, Required] AudioClip crashSFX;
        [SerializeField, Required] AudioClip connectionSFX;

        public void PlayCrashSFX()
        {
            if (crashSFX != null)
                audioSource.PlayOneShot(crashSFX);
        }

        public void PlayConnectionSFX()
        {
            if (connectionSFX != null)
                audioSource.PlayOneShot(connectionSFX);
        }
    }
}