using UnityEngine;
using UnityEngine.UI;

public class CreditsMenuPresenter : MonoBehaviour
{
    [SerializeField] Button toTitleScreenButton;
    [SerializeField] ButtonSoundPlayer buttonSoundPlayer;

    SceneLoaderBrain sceneLoader;
    CursorPresenter cursorController;

    void Start()
    {
        sceneLoader = CoreSystems.Instance.SceneLoader;
        cursorController = OverlaySystems.Instance.CursorPresenter;
        SetupButtons();
    }

    void SetupButtons()
    {
        toTitleScreenButton.onClick.AddListener(OnClickToTitleScreen);
    }

    void OnClickToTitleScreen()
    {
        buttonSoundPlayer.PlaySelectSFX();
        sceneLoader.GoToTitleScreen();
    }
}
