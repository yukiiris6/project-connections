using UnityEngine;
using Sirenix.OdinInspector;

public class ButtonSoundPlayer : MonoBehaviour
{
    [SerializeField, Required] AudioSource audioSource;
    [SerializeField, Required] AudioClip selectSFX;
    [SerializeField, Required] AudioClip pressSFX;
    [SerializeField, Required] AudioClip levelSelectSFX;
    [SerializeField, Required] AudioClip backButtonPressSFX;

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