using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TitleScreenFlow : MonoBehaviour
{
    [SerializeField] GameObject titleScreenMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] AudioClip backButtonPressSFX;
    [SerializeField] AudioClip buttonPressSFX;
    [SerializeField] AudioClip levelSelectSFX;

    AudioSource audioSource;
    CursorController cursorController;
    LevelManager levelManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        cursorController = OverlayCanvas.Instance.CursorController;
        levelManager = GlobalSystems.Instance.LevelManager;
    }

    public void OnClickStart()
    {
        if (levelManager.IsLoading) return;
        audioSource.PlayOneShot(buttonPressSFX);
        titleScreenMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
        cursorController.ChangeToNormalCursor();
    }

    public void OnClickCredits()
    {
        if (levelManager.IsLoading) return;
        audioSource.PlayOneShot(buttonPressSFX);
        levelManager.GoToCredits(true);
        cursorController.ChangeToNormalCursor();
        cursorController.HideCursor();
    }

    public void OnClickExit()
    {
        if (levelManager.IsLoading) return;
        audioSource.PlayOneShot(buttonPressSFX);
        cursorController.ChangeToNormalCursor();
        Application.Quit();
        if (Application.isEditor) print("Exiting game...");
    }

    public void OnClickLevel(string name, string displayName)
    {
        if (levelManager.IsLoading) return;
        audioSource.PlayOneShot(levelSelectSFX);
        cursorController.ChangeToNormalCursor();
        cursorController.HideCursor();
        levelSelectMenu.SetActive(false);
        levelManager.LoadLevel(name, displayName);
    }

    public void OnClickBack()
    {
        if (levelManager.IsLoading) return;
        audioSource.PlayOneShot(backButtonPressSFX);
        cursorController.ChangeToNormalCursor();
        titleScreenMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
    }
}
