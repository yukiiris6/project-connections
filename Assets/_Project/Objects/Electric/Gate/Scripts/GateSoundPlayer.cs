using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class GateSoundPlayer : MonoBehaviour
    {
        [SerializeField, Required] AudioClip gateSFX;
        [SerializeField, Required] AudioSource audioSource;

        public void PlayGateSFX()
        {
            audioSource.PlayOneShot(gateSFX);
        }
    }
}
