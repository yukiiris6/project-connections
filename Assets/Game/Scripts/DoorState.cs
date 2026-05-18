using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorState : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Animator doorAnimator;
    [SerializeField] Light2D light2D;
    [SerializeField] Color closedLightColor;
    [SerializeField] Color openLightColor;
    [SerializeField] string IsOpenBool = "IsOpen";

    public bool isDoorActive = false;
    public bool isInsideDoor = false;

    public void SetActive(bool active)
    {
        isDoorActive = active;
        SetAnimatorState();
        SetColor();
    }

    void SetAnimatorState()
    {
        doorAnimator.SetBool(IsOpenBool, isDoorActive);
    }

    void SetColor()
    {
        light2D.color = isDoorActive ? openLightColor : closedLightColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && isDoorActive)
        {
            isInsideDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && isDoorActive)
        {
            isInsideDoor = false;
        }
    }
}
