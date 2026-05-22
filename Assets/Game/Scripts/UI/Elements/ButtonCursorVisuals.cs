using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonCursorVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] AudioClip selectSFX;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetDependencies();
        if (cursorController != null)
        {
            if (button.interactable == true)
            {
                cursorController.ChangeToInteractionCursor();
                audioSource.PlayOneShot(selectSFX);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetDependencies();
        if (cursorController != null)
        {
            if (button.interactable == true) cursorController.ChangeToNormalCursor();
        }
    }
}
