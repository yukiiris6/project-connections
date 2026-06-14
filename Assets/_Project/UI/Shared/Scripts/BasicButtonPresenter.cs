using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicButtonPresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ButtonHighlightAnimation buttonHighlightAnimation;
    [SerializeField] Button button;

    CursorPresenter cursorController;

    void Start()
    {
        GetDependencies();
        button.onClick.AddListener(BasicButtonClick);
    }

    void GetDependencies()
    {
        if (cursorController != null) return;
        cursorController = OverlaySystems.Instance.CursorPresenter;
    }

    public void Init(ButtonHighlightAnimation buttonHighlightAnimation)
    {
        this.buttonHighlightAnimation = buttonHighlightAnimation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetDependencies();
        if (button.interactable == true)
        {
            cursorController.ChangeToInteractionCursor();
            buttonHighlightAnimation.ShowHighlight();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetDependencies();
        if (button.interactable == true)
        {
            cursorController.ChangeToNormalCursor();
            buttonHighlightAnimation.HideHighlight();
        }
    }

    void BasicButtonClick()
    {
        cursorController.ChangeToNormalCursor();
        cursorController.HideCursor();
    }
}
