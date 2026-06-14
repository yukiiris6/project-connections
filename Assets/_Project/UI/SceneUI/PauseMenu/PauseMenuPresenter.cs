using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuPresenter : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject dimBackgroundObject;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pauseSFX;
    [SerializeField] PlayerInputMapper playerInputMapper;

    [Header("Button References")]
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button toTitleButton;

    GameStateSetterBrain gameStateSetter;
    SceneLoaderBrain sceneLoader;
    MusicPlayer musicPlayer;

    void Start()
    {
        gameStateSetter = CoreSystems.Instance.GameStateSetter;
        sceneLoader = CoreSystems.Instance.SceneLoader;
        musicPlayer = CoreSystems.Instance.MusicPlayer;
        SetupButtons();
    }

    void SetupButtons()
    {
        resumeButton.onClick.AddListener(OnClickRestart);
        restartButton.onClick.AddListener(OnClickRestart);
        toTitleButton.onClick.AddListener(OnClickToTitle);
    }

    bool IsOpen()
    {
        return pauseMenu.activeInHierarchy;
    }

    public void ToggleMenu()
    {
        if (IsOpen()) OpenMenu();
        else CloseMenu();
    }

    void OpenMenu()
    {
        dimBackgroundObject.SetActive(true);
        pauseMenu.SetActive(true);
        audioSource.PlayOneShot(pauseSFX);
        musicPlayer.PauseMusic();
        gameStateSetter.PauseGame();
        playerInputMapper.SetUIActionMap();
    }

    void CloseMenu()
    {
        dimBackgroundObject.SetActive(false);
        pauseMenu.SetActive(false);
        musicPlayer.PlayMusic();
        gameStateSetter.ResumeGame();
        playerInputMapper.SetGameplayActionMap();
    }

    void OnClickResume()
    {
        CloseMenu();
    }

    void OnClickRestart()
    {
        dimBackgroundObject.SetActive(false);
        pauseMenu.SetActive(false);
        sceneLoader.RestartLevel();
    }

    void OnClickToTitle()
    {
        dimBackgroundObject.SetActive(false);
        pauseMenu.SetActive(false);
        sceneLoader.GoToTitleScreen();
    }
}
