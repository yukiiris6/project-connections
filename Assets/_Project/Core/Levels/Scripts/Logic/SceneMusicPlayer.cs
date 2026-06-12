using UnityEngine;

public class SceneMusicPlayer : MonoBehaviour
{
    MusicPlayer musicPlayer;

    void GetDependencies()
    {
        if (musicPlayer != null) return;
        musicPlayer = CoreSystems.Instance.MusicPlayer;
    }

    public void StopMusic()
    {
        GetDependencies();
        musicPlayer.StopMusic();
    }

    public void PlayMenuMusic()
    {
        musicPlayer.SetTitleScreenMusic();
        musicPlayer.PlayMusic();
    }

    public void PlayLevelMusic()
    {
        musicPlayer.SetLevelMusic();
        musicPlayer.PlayMusic();
    }
}