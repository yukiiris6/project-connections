using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPresenter : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] GameObject titleScreenMenu;
    [SerializeField] GameObject levelButtonPrefab;
    [SerializeField] Transform levelButtonParent;
    [SerializeField] TMP_Text levelNameText;

    [Header("Button References")]
    [SerializeField] Button backButton;
    [SerializeField] ButtonHighlightAnimation buttonHighlightAnimation;
    [SerializeField] ButtonSoundPlayer buttonSoundPlayer;

    CursorPresenter cursorController;
    SceneLoaderBrain sceneLoader;

    void Start()
    {
        cursorController = OverlaySystems.Instance.CursorPresenter;
        sceneLoader = CoreSystems.Instance.SceneLoader;
    }

    void SetupButtons()
    {
        InstantiateLevelButtons();
        backButton.onClick.AddListener(OnClickBack);
    }

    void InstantiateLevelButtons()
    {
        LevelData[] levels = CoreSystems.Instance.SceneLoader.LevelDataStorage.Levels;
        for (int i = 0; i < levels.Count(); i++)
        {
            CreateButton(levels[i], i + 1);
        }
    }

    void CreateButton(LevelData levelData, int index)
    {
        var buttonObject = Instantiate(levelButtonPrefab, Vector2.zero, Quaternion.identity, levelButtonParent);
        LevelButtonPresenter levelButtonPresenter = buttonObject.GetComponent<LevelButtonPresenter>();
        levelButtonPresenter.Init(levelNameText, levelData.DisplayName, levelData.State);
        levelButtonPresenter.BasicButtonPresenter.Init(buttonHighlightAnimation);
        levelButtonPresenter.button.onClick.AddListener(() => OnClickLevel(levelData.FileName));
    }

    void OnClickLevel(string name)
    {
        buttonSoundPlayer.PlayLevelSelectSFX();
        sceneLoader.GoToLevel(name);
        gameObject.SetActive(false);
    }

    void OnClickBack()
    {
        buttonSoundPlayer.PlayBackButtonPressSFX();
        cursorController.ChangeToNormalCursor();
        titleScreenMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
