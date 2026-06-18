using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonPresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [field: SerializeField] public Button button { get; private set; }
    [field: SerializeField] public TMP_Text labelText { get; private set; }
    [field: SerializeField] public BasicButtonPresenter BasicButtonPresenter { get; private set; }

    [Header("Visuals")]
    [SerializeField, Required] Sprite defaultSprite;
    [SerializeField, Required] Sprite finishedSprite;
    [SerializeField, Required] Sprite lockedSprite;
    [SerializeField, Required] Color lockedColor;

    TMP_Text levelNameText;
    string levelName;

    void SetupComponent(LevelState levelState, int index)
    {
        switch (levelState)
        {
            case LevelState.Locked:
                labelText.color = lockedColor;
                button.image.sprite = lockedSprite;
                button.interactable = false;
                break;
            case LevelState.Finished:
                button.image.sprite = finishedSprite;
                break;
            default:
                button.image.sprite = defaultSprite;
                break;
        }
        labelText.text = index.ToString();
    }

    public void Init(TMP_Text levelNameText, string levelName, LevelState levelState, int index)
    {
        this.levelNameText = levelNameText;
        this.levelName = levelName;
        SetupComponent(levelState, index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable == true && levelNameText != null)
        {
            levelNameText.text = levelName;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable == true && levelNameText != null)
        {
            levelNameText.text = "";
        }
    }
}
