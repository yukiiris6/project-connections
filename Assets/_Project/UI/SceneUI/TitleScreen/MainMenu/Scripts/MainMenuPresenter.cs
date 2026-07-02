using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using ProjectConnections.UIShared;
using ProjectConnections.Core;
using ProjectConnections.UI.Overlay;

namespace ProjectConnections.SceneUI
{
    public class MainMenuPresenter : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField, Required] GameObject levelSelectMenu;

        [Header("Button References")]
        [SerializeField, Required] Button startButton;
        [SerializeField, Required] Button creditsButton;
        [SerializeField, Required] Button exitButton;
        [SerializeField, Required] ButtonSoundPlayer buttonSoundPlayer;

        SceneLoaderBrain sceneLoader;
        CursorPresenter cursorPresenter;

        void GetDependencies()
        {
            if (sceneLoader != null && cursorPresenter != null) return;
            sceneLoader = CoreSystems.Instance.SceneLoaderBrain;
            cursorPresenter = OverlaySystems.Instance.CursorPresenter;
        }

        void Start()
        {
            GetDependencies();
            SetupButtons();
        }

        void OnEnable()
        {
            GetDependencies();
            sceneLoader.SceneLoader.OnLevelLoad += cursorPresenter.ShowCursor;
        }

        void OnDisable()
        {
            sceneLoader.SceneLoader.OnLevelLoad -= cursorPresenter.ShowCursor;
        }

        void SetupButtons()
        {
            startButton.onClick.AddListener(OnClickStart);
            creditsButton.onClick.AddListener(OnClickCredits);
            exitButton.onClick.AddListener(OnClickExit);
        }

        void OnClickStart()
        {
            buttonSoundPlayer.PlayPressSFX();
            levelSelectMenu.SetActive(true);
            gameObject.SetActive(false);
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
}
