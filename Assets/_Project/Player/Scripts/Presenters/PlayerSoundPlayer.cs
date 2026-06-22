using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class PlayerSoundPlayer : MonoBehaviour
    {
        [field: SerializeField] AudioSource audioSource;
        [SerializeField, Required] AudioClip jumpSFX;

        public void PlayJumpSFX()
        {
            audioSource.PlayOneShot(jumpSFX);
        }
    }
}
