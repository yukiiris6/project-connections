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
    SceneLoaderBrain sceneLoader;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        cursorController = UISystems.Instance.OverlayCanvas.CursorController;
        sceneLoader = CoreSystems.Instance.SceneLoader;
    }

    public void OnClickStart()
    {
        audioSource.PlayOneShot(buttonPressSFX);
        titleScreenMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
        cursorController.ChangeToNormalCursor();
    }

    public void OnClickCredits()
    {
        audioSource.PlayOneShot(buttonPressSFX);
        sceneLoader.GoToCredits();
        cursorController.ChangeToNormalCursor();
        cursorController.HideCursor();
    }

    public void OnClickExit()
    {
        audioSource.PlayOneShot(buttonPressSFX);
        cursorController.ChangeToNormalCursor();
        Application.Quit();
        if (Application.isEditor) print("Exiting game...");
    }

    public void OnClickLevel(string name)
    {
        audioSource.PlayOneShot(levelSelectSFX);
        cursorController.ChangeToNormalCursor();
        cursorController.HideCursor();
        levelSelectMenu.SetActive(false);
        sceneLoader.GoToLevel(name);
    }

    public void OnClickBack()
    {
        audioSource.PlayOneShot(backButtonPressSFX);
        cursorController.ChangeToNormalCursor();
        titleScreenMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
    }
}
