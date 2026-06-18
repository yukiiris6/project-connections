using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class CreditsMenuPresenter : MonoBehaviour
{
    [SerializeField, Required] Button toTitleScreenButton;
    [SerializeField, Required] ButtonSoundPlayer buttonSoundPlayer;

    SceneLoaderBrain sceneLoader;

    void GetDependencies()
    {
        if (sceneLoader != null) return;
        sceneLoader = CoreSystems.Instance.SceneLoader;
    }

    void Start()
    {
        GetDependencies();
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
