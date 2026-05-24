using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip titleScreenMusic;
    [SerializeField] AudioClip levelMusic;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (GlobalSystems.Instance.LevelManager.IsinTitleScreen)
        {
            SetTitleScreenMusic();
        }
        else
        {
            SetLevelMusic();
        }
        PlayMusic();
    }

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
