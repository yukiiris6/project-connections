using UnityEngine;
using UnityEngine.UI;

public class MainMenuPresenter : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] GameObject titleScreenMenu;
    [SerializeField] GameObject levelSelectMenu;

    [Header("Button References")]
    [SerializeField] Button startButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button exitButton;
    [SerializeField] ButtonSoundPlayer buttonSoundPlayer;

    SceneLoaderBrain sceneLoader;

    void Start()
    {
        sceneLoader = CoreSystems.Instance.SceneLoader;
        SetupButtons();
    }

    void SetupButtons()
    {
        startButton.onClick.AddListener(OnClickStart);
        creditsButton.onClick.AddListener(OnClickStart);
        exitButton.onClick.AddListener(OnClickStart);
    }

    void OnClickStart()
    {
        buttonSoundPlayer.PlayPressSFX();
        titleScreenMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
    }

    void OnClickCredits()
    {
        buttonSoundPlayer.PlayPressSFX();
        sceneLoader.GoToCredits();
    }

    void OnClickExit()
    {
        buttonSoundPlayer.PlayPressSFX();
        Application.Quit();
        if (Application.isEditor) print("Exiting game...");
    }
}
