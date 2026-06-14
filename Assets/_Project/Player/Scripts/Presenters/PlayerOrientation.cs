using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAnimatorHandler playerAnimation;


    public void SetFacing(float x)
    {
        if (x == 0) return;
        bool isFacingLeft = x < 0;
        playerAnimation.UpdateFacing(isFacingLeft);
    }
}