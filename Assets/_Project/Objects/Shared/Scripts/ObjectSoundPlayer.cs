using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class ObjectSoundPlayer : MonoBehaviour
    {
        [SerializeField, Required] AudioSource audioSource;
        [SerializeField, Required] AudioClip crashSFX;
        [SerializeField, Required] AudioClip connectionSFX;
        [SerializeField] AudioClip dropSFX;
        [SerializeField] AudioClip throwSFX;

        public void PlayCrashSFX()
        {
            audioSource.PlayOneShot(crashSFX);
        }

        public void PlayConnectionSFX()
        {
            audioSource.PlayOneShot(connectionSFX);
        }

        public void PlayDropSFX()
        {
            audioSource.PlayOneShot(dropSFX);
        }

        public void PlayThrowSFX()
        {
            audioSource.PlayOneShot(throwSFX);
        }
    }
}
