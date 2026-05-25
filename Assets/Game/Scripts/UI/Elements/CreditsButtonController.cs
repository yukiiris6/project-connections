using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CreditsButton : MonoBehaviour
{
    [SerializeField] AudioClip buttonPressSFX;

    LevelManager levelManager;
    AudioSource audioSource;
    CursorController cursorController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelManager = GlobalSystems.Instance.LevelManager;
        cursorController = OverlayCanvas.Instance.CursorController;
    }

    public void OnClick()
    {
        if (levelManager.IsLoading) return;
        audioSource.PlayOneShot(buttonPressSFX);
        levelManager.GoToTitleScreen(false);
        cursorController.ChangeToNormalCursor();
        cursorController.HideCursor();
    }
}
