using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    [field: SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jumpSFX;

    public void PlayJumpSFX()
    {
        audioSource.PlayOneShot(jumpSFX);
    }
}
