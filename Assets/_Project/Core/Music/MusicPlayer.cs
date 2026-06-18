using UnityEngine;
using Sirenix.OdinInspector;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField, Required] AudioSource audioSource;

    public void SetAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic() => audioSource.Stop();

    public void PauseMusic() => audioSource.Pause();
}
