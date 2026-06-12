using UnityEngine;

namespace ProjectConnections.Electric
{
    public class GateSoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip gateSFX;
        [SerializeField] AudioSource audioSource;

        public void PlayGateSFX()
        {
            audioSource.PlayOneShot(gateSFX);
        }
    }
}
