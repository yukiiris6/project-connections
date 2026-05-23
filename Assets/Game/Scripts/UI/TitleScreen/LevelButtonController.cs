using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color lockedColor;

    Button button;
    TMP_Text labelText;
    TMP_Text levelNameText;
    string levelName;
    bool isLocked;

    void GetDependencies()
    {
        if (button != null && labelText != null) return;
        button = GetComponent<Button>();
        labelText = GetComponentInChildren<TMP_Text>();
    }

    void Awake() => GetDependencies();

    public void Init(TMP_Text newLevelNameText, string newLevelName, bool newIsLocked)
    {
        levelNameText = newLevelNameText;
        levelName = newLevelName;
        isLocked = newIsLocked;
        SetupComponent();
    }

    void SetupComponent()
    {
        GetDependencies();
        if (isLocked)
        {
            labelText.color = lockedColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (levelNameText != null)
        {
            if (button.interactable == true) levelNameText.text = levelName;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (levelNameText != null)
        {
            if (button.interactable == true) levelNameText.text = "";
        }
    }
}
