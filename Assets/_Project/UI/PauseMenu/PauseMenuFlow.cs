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
    GameManager gameManager;
    LevelManager levelManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        cursorController = OverlayCanvas.Instance.CursorController;
        gameManager = GlobalSystems.Instance.GameManager;
        levelManager = GlobalSystems.Instance.LevelManager;
    }

    public void OpenMenu()
    {
        GlobalSystems.Instance.MusicManager.PauseMusic();
        audioSource.PlayOneShot(pauseSFX);
        pauseMenuContainer.SetActive(true);
        backgroundObject.SetActive(true);
    }

    public void OnClickResume()
    {
        GlobalSystems.Instance.MusicManager.PlayMusic();
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
        levelManager.RestartLevel();
        cursorController.ChangeToNormalCursor();
    }

    public void OnClickToTitle()
    {
        PlayClickSound();
        backgroundObject.SetActive(false);
        pauseMenuContainer.SetActive(false);
        levelManager.GoToTitleScreen(true);
        cursorController.ChangeToNormalCursor();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(buttonPressSFX);
    }
}
