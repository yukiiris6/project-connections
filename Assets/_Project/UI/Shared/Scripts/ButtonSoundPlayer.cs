using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip selectSFX;
    [SerializeField] AudioClip pressSFX;
    [SerializeField] AudioClip levelSelectSFX;
    [SerializeField] AudioClip backButtonPressSFX;

    public void PlaySelectSFX()
    {
        audioSource.PlayOneShot(selectSFX);
    }

    public void PlayPressSFX()
    {
        audioSource.PlayOneShot(pressSFX);
    }

    public void PlayLevelSelectSFX()
    {
        audioSource.PlayOneShot(levelSelectSFX);
    }

    public void PlayBackButtonPressSFX()
    {
        audioSource.PlayOneShot(backButtonPressSFX);
    }
}