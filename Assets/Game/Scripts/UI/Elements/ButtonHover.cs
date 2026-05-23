using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] AudioClip selectSFX;
    [SerializeField] GameObject selectionImage;

    AudioSource audioSource;
    CursorController cursorController;
    Button button;

    void GetDependencies()
    {
        if (cursorController != null && button != null && audioSource != null) return;
        audioSource = GetComponent<AudioSource>();
        cursorController = OverlayCanvas.Instance.CursorController;
        button = GetComponent<Button>();
    }

    public void InsertDependencies(GameObject newSelectionImage)
    {
        selectionImage = newSelectionImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetDependencies();
        if (button.interactable == true)
        {
            audioSource.PlayOneShot(selectSFX);
            if (cursorController != null) cursorController.ChangeToInteractionCursor();
            if (selectionImage != null)
            {
                selectionImage.SetActive(true);
                selectionImage.transform.position = transform.position;
                selectionImage.transform.DOKill();
                selectionImage.transform.localScale = new(1f, 1f, 1f);
                selectionImage.transform.DOScale(1.05f, .5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetDependencies();
        if (button.interactable == true)
        {
            if (cursorController != null) cursorController.ChangeToNormalCursor();
            if (selectionImage != null)
            {
                selectionImage.SetActive(false);
                selectionImage.transform.DOKill();
            }
        }
    }

    public void CancelAllEffects()
    {
        selectionImage.transform.DOKill();
        selectionImage.transform.localScale = new(1f, 1f, 1f);
        selectionImage.SetActive(false);
    }
}
