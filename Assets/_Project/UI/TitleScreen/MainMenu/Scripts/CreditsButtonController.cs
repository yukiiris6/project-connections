using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CreditsButton : MonoBehaviour
{
    [SerializeField] AudioClip buttonPressSFX;

    SceneLoaderBrain sceneLoader;
    AudioSource audioSource;
    CursorController cursorController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sceneLoader = CoreSystems.Instance.SceneLoader;
        cursorController = UISystems.Instance.OverlayCanvas.CursorController;
    }

    public void OnClick()
    {
        audioSource.PlayOneShot(buttonPressSFX);
        sceneLoader.GoToTitleScreen();
        cursorController.ChangeToNormalCursor();
        cursorController.HideCursor();
    }
}
