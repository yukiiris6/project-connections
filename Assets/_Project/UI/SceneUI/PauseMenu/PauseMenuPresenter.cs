using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using ProjectConnections.UIShared;
using ProjectConnections.Core;
using ProjectConnections.Player;

namespace ProjectConnections.SceneUI
{
    public class PauseMenuPresenter : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField, Required] GameObject pauseMenu;
        [SerializeField, Required] GameObject dimBackgroundObject;
        [SerializeField, Required] AudioSource audioSource;
        [SerializeField, Required] AudioClip pauseSFX;
        [SerializeField, Required] PlayerController playerController;
        [SerializeField, Required] PlayerInputMapper playerInputMapper;

        [Header("Button References")]
        [SerializeField, Required] Button resumeButton;
        [SerializeField, Required] Button restartButton;
        [SerializeField, Required] Button toTitleButton;

        GameStateSetterBrain gameStateSetter;
        SceneLoaderBrain sceneLoader;
        MusicPlayer musicPlayer;

        void SetupButtons()
        {
            resumeButton.onClick.AddListener(OnClickResume);
            restartButton.onClick.AddListener(OnClickRestart);
            toTitleButton.onClick.AddListener(OnClickToTitle);
        }

        void Start()
        {
            gameStateSetter = CoreSystems.Instance.GameStateSetter;
            sceneLoader = CoreSystems.Instance.SceneLoaderBrain;
            musicPlayer = CoreSystems.Instance.MusicPlayer;
            SetupButtons();
        }

        void OnEnable()
        {
            playerController.OnPauseInput += ToggleMenu;
        }

        void OnDisable()
        {
            playerController.OnPauseInput -= ToggleMenu;
        }

        public void ToggleMenu(bool isPressed)
        {
            if (pauseMenu.activeInHierarchy) CloseMenu();
            else OpenMenu();
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
}
