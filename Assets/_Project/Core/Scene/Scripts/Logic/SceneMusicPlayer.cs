using UnityEngine;
using Sirenix.OdinInspector;

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

    public void SetMusicAndPlay(AudioClip audioClip)
    {
        GetDependencies();
        musicPlayer.SetAudioClip(audioClip);
        musicPlayer.PlayMusic();
    }
}