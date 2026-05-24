using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class CursorController : MonoBehaviour
{
    [SerializeField] Sprite normalCursor;
    [SerializeField] Sprite aimingCursor;
    [SerializeField] Sprite magnetCursor;

    RectTransform rectTransform;
    Image cursorImage;

    void Start()
    {
        cursorImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        Cursor.visible = false;
    }

    void Update()
    {
        FollowCursor();
    }

    void FollowCursor()
    {
        Cursor.visible = false;
        cursorImage.rectTransform.position = Mouse.current.position.ReadValue();
    }


    public void ShowCursor()
    {
        RestoreCursor();
        cursorImage.enabled = true;
    }

    public void HideCursor()
    {
        RestoreCursor();
        cursorImage.enabled = false;
    }

    public void ChangeToNormalCursor()
    {
        RestoreCursor();
        rectTransform.pivot = new(0, 1f);
        cursorImage.sprite = normalCursor;
        rectTransform.rotation = Quaternion.identity;
    }

    public void ChangeToInteractionCursor()
    {
        RestoreCursor();
        rectTransform.pivot = new(.5f, .8f);
        cursorImage.sprite = magnetCursor;
        rectTransform.rotation = Quaternion.identity;
    }

    public void ChangeToAimingCursor()
    {
        RestoreCursor();
        cursorImage.sprite = aimingCursor;
        rectTransform.pivot = new(.5f, .5f);
    }

    public void ChangeToMagnetizeCursor(Vector2 direction)
    {
        RestoreCursor();
        cursorImage.sprite = magnetCursor;
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
