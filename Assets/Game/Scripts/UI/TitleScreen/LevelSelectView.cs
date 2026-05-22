using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectView : MonoBehaviour
{
    [SerializeField] TitleScreenFlow titleScreenFlow;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Sprite levelButton;
    [SerializeField] Sprite finishedLevelButton;
    [SerializeField] Sprite lockedLevelButton;
    [SerializeField] Transform buttonParent;
    [SerializeField] TMP_Text levelNameText;

    void Start()
    {
        LevelData[] levels = GlobalSystems.Instance.LevelManager.Levels;
        for (int i = 0; i < levels.Count(); i++)
        {
            CreateButton(levels[i], i + 1);
        }
    }

    void CreateButton(LevelData levelData, int index)
    {
        var buttonObject = Instantiate(buttonPrefab, Vector2.zero, Quaternion.identity, buttonParent);
        var button = buttonObject.GetComponent<Button>();
        var levelText = buttonObject.GetComponentInChildren<TMP_Text>();
        button.onClick.AddListener(() => titleScreenFlow.OnClickLevel(levelData.fileName));
        levelText.text = index.ToString();
        button.image.sprite = levelButton;
        if (levelData.isLocked)
        {
            button.image.sprite = lockedLevelButton;
            button.interactable = false;
        }
        if (levelData.isFinished)
        {
            button.image.sprite = finishedLevelButton;
        }
        buttonObject.GetComponent<LevelButtonController>().Init(levelNameText, levelData.levelDisplayName);
    }
}
