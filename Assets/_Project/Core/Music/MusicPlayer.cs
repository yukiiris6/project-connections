using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

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
