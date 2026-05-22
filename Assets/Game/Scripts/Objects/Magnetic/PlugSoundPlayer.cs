using UnityEngine;

public class PlugSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip impactSFX;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayImpactSFX()
    {
        audioSource.PlayOneShot(impactSFX);
    }
}
