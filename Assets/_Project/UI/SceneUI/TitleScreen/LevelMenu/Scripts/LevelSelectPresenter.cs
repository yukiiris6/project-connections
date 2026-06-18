using System.Linq;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class LevelSelectPresenter : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField, Required] GameObject titleScreenMenu;
    [SerializeField, Required] GameObject levelButtonPrefab;
    [SerializeField, Required] Transform levelButtonParent;
    [SerializeField, Required] TMP_Text levelNameText;

    [Header("Button References")]
    [SerializeField, Required] Button backButton;
    [SerializeField, Required] SelectionHighlightAnimation buttonHighlightAnimation;
    [SerializeField, Required] ButtonSoundPlayer buttonSoundPlayer;

    CursorPresenter cursorController;
    SceneLoaderBrain sceneLoader;

    void Start()
    {
        cursorController = OverlaySystems.Instance.CursorPresenter;
        sceneLoader = CoreSystems.Instance.SceneLoader;
        SetupButtons();
    }

    void SetupButtons()
    {
        InstantiateLevelButtons();
        backButton.onClick.AddListener(OnClickBack);
    }

    void InstantiateLevelButtons()
    {
        LevelData[] levels = sceneLoader.LevelDataStorage.GetLevels();
        int index = 0;
        foreach (var level in levels)
        {
            if (level.Type != LevelType.Stage) continue;
            index++;
            CreateButton(level, index);
        }
    }

    void CreateButton(LevelData levelData, int index)
    {
        var buttonObject = Instantiate(levelButtonPrefab, Vector2.zero, Quaternion.identity, levelButtonParent);
        LevelButtonPresenter levelButtonPresenter = buttonObject.GetComponent<LevelButtonPresenter>();
        levelButtonPresenter.Init(levelNameText, levelData.DisplayName, levelData.State, index);
        levelButtonPresenter.BasicButtonPresenter.Init(buttonHighlightAnimation, buttonSoundPlayer);
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
