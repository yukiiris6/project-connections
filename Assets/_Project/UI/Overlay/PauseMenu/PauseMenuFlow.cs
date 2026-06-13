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
    MusicPlayer musicPlayer;
    PlayerInputMapper playerInputMapper;

    bool IsOpen;

    void GetDependencies()
    {
        if (playerInputMapper != null && audioSource != null) return;
        playerInputMapper = FindFirstObjectByType<PlayerInputMapper>();
        audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        GetDependencies();
    }

    void Start()
    {
        cursorController = UISystems.Instance.OverlayCanvas.CursorController;
        gameManager = CoreSystems.Instance.GameBrain;
        sceneLoader = CoreSystems.Instance.SceneLoader;
        musicPlayer = CoreSystems.Instance.MusicPlayer;
    }

    public void ToggleMenu()
    {
        GetDependencies();
        if (IsOpen) OpenMenu();
        else CloseMenu();
    }

    public void OpenMenu()
    {
        GetDependencies();
        musicPlayer.PauseMusic();
        audioSource.PlayOneShot(pauseSFX);
        pauseMenuContainer.SetActive(true);
        backgroundObject.SetActive(true);
        gameManager.PauseGame();
        playerInputMapper.SetUIActionMap();
    }

    public void CloseMenu()
    {
        GetDependencies();
        musicPlayer.PlayMusic();
        PlayClickSound();
        backgroundObject.SetActive(false);
        pauseMenuContainer.SetActive(false);
        gameManager.ResumeGame();
        cursorController.ChangeToNormalCursor();
        playerInputMapper.SetGameplayActionMap();
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
