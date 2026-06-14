using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorPresenter : MonoBehaviour
{
    [Header("Cursor Sprites")]
    [SerializeField] Sprite normalCursor;
    [SerializeField] Sprite aimingCursor;
    [SerializeField] Sprite magnetCursor;

    [Header("References")]
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image cursorImageComponent;

    void Start()
    {
        Cursor.visible = false;
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
        RestoreCursor();
        cursorImageComponent.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideCursor()
    {
        RestoreCursor();
        cursorImageComponent.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ChangeToNormalCursor()
    {
        RestoreCursor();
        rectTransform.pivot = new(0, 1f);
        cursorImageComponent.sprite = normalCursor;
        rectTransform.rotation = Quaternion.identity;
    }

    public void ChangeToInteractionCursor()
    {
        RestoreCursor();
        rectTransform.pivot = new(.5f, .8f);
        cursorImageComponent.sprite = magnetCursor;
        rectTransform.rotation = Quaternion.identity;
    }

    public void ChangeToAimingCursor()
    {
        RestoreCursor();
        cursorImageComponent.sprite = aimingCursor;
        rectTransform.pivot = new(.5f, .5f);
    }

    public void ChangeToMagnetizeCursor(Vector2 direction)
    {
        RestoreCursor();
        cursorImageComponent.sprite = magnetCursor;
        rectTransform.pivot = new(.5f, .5f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0f, 0f, angle - 110f);
    }

    void RestoreCursor()
    {
        StopAllCoroutines();
        rectTransform.pivot = new(0, 1f);
        rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
