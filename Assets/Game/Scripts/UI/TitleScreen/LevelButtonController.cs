using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    TMP_Text levelNameText;
    string levelName;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Init(TMP_Text newLevelNameText, string newLevelName)
    {
        levelNameText = newLevelNameText;
        levelName = newLevelName;
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
