using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerSoundPlayer : MonoBehaviour
{
    [field: SerializeField] AudioSource audioSource;
    [SerializeField, Required] AudioClip jumpSFX;

    public void PlayJumpSFX()
    {
        audioSource.PlayOneShot(jumpSFX);
    }
}
