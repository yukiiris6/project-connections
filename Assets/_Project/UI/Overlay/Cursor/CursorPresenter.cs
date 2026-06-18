using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CursorPresenter : MonoBehaviour
{
    [Header("Cursor Sprites")]
    [SerializeField, Required] Sprite normalCursor;
    [SerializeField, Required] Sprite aimingCursor;
    [SerializeField, Required] Sprite magnetCursor;

    [Header("References")]
    [SerializeField, Required] RectTransform rectTransform;
    [SerializeField, Required] Image cursorImageComponent;

    EventSystem eventSystem;

    void GetComponents()
    {
        if (eventSystem != null) return;
        eventSystem = UnityEngine.EventSystems.EventSystem.current;
    }

    void Start()
    {
        Cursor.visible = false;
        GetComponents();
    }

    void Update()
    {
        FollowCursor();
    }

    void FollowCursor()
    {
        Cursor.visible = false;
        cursorImageComponent.rectTransform.position = Mouse.current.position.ReadValue();
    }

    public void ShowCursor()
    {
        RestoreEverything();
        cursorImageComponent.enabled = true;
        AllowInteractions();
    }

    public void HideCursor()
    {
        RestoreEverything();
        cursorImageComponent.enabled = false;
        UnallowInteractions();
    }

    public void AllowInteractions()
    {
        GetComponents();
        eventSystem.enabled = true;
    }

    public void UnallowInteractions()
    {
        GetComponents();
        eventSystem.enabled = false;
    }

    public void ChangeToNormalCursor()
    {
        RestoreEverything();
        rectTransform.pivot = new(0, 1f);
        cursorImageComponent.sprite = normalCursor;
        rectTransform.rotation = Quaternion.identity;
    }

    public void ChangeToInteractionCursor()
    {
        RestoreEverything();
        rectTransform.pivot = new(.5f, .8f);
        cursorImageComponent.sprite = magnetCursor;
        rectTransform.rotation = Quaternion.identity;
    }

    public void ChangeToAimingCursor()
    {
        RestoreEverything();
        cursorImageComponent.sprite = aimingCursor;
        rectTransform.pivot = new(.5f, .5f);
    }

    public void ChangeToMagnetizeCursor(Vector2 direction)
    {
        RestoreEverything();
        cursorImageComponent.sprite = magnetCursor;
        rectTransform.pivot = new(.5f, .5f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0f, 0f, angle - 110f);
    }

    void RestoreEverything()
    {
        GetComponents();
        rectTransform.pivot = new(0, 1f);
        rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
