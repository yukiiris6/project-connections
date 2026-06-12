using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip titleScreenMusic;
    [SerializeField] AudioClip levelMusic;

    public void SetTitleScreenMusic()
    {
        audioSource.clip = titleScreenMusic;
    }

    public void SetLevelMusic()
    {
        audioSource.clip = levelMusic;
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic() => audioSource.Stop();

    public void PauseMusic() => audioSource.Pause();
}
