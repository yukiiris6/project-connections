using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseMenuFlow : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuContainer;
    [SerializeField] GameObject backgroundObject;
    [SerializeField] AudioClip buttonPressSFX;
    [SerializeField] AudioClip pauseSFX;

    AudioSource audioSource;
    CursorController cursorController;
    GameBrain gameManager;
    SceneLoaderBrain sceneLoader;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        cursorController = UISystems.Instance.OverlayCanvas.CursorController;
        gameManager = CoreSystems.Instance.GameBrain;
        sceneLoader = CoreSystems.Instance.SceneLoader;
    }

    public void OpenMenu()
    {
        CoreSystems.Instance.MusicPlayer.PauseMusic();
        audioSource.PlayOneShot(pauseSFX);
        pauseMenuContainer.SetActive(true);
        backgroundObject.SetActive(true);
    }

    public void OnClickResume()
    {
        CoreSystems.Instance.MusicPlayer.PlayMusic();
        PlayClickSound();
        backgroundObject.SetActive(false);
        pauseMenuContainer.SetActive(false);
        gameManager.ResumeGame();
        cursorController.ChangeToNormalCursor();
    }

    public void OnClickRestart()
    {
        PlayClickSound();
        backgroundObject.SetActive(false);
        pauseMenuContainer.SetActive(false);
        sceneLoader.RestartLevel();
        cursorController.ChangeToNormalCursor();
    }

    public void OnClickToTitle()
    {
        PlayClickSound();
        backgroundObject.SetActive(false);
        pauseMenuContainer.SetActive(false);
        sceneLoader.GoToTitleScreen();
        cursorController.ChangeToNormalCursor();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(buttonPressSFX);
    }
}
