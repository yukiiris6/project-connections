using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class PlayerSoundPlayer : MonoBehaviour
    {
        [field: SerializeField] AudioSource audioSource;
        [SerializeField, Required] AudioClip jumpSFX;
        [SerializeField, Required] AudioClip deathSFX;

        public void PlayJumpSFX()
        {
            audioSource.PlayOneShot(jumpSFX);
        }

        public void PlayDeathSFX()
        {
            audioSource.PlayOneShot(deathSFX);
        }
    }
}
