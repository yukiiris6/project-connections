using UnityEngine;

namespace ProjectConnections.Magnetism
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip crashSFX;
        [SerializeField] AudioClip connectionSFX;

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